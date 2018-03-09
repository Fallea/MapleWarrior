using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;

namespace Net
{
    public enum SocketState
    {
        Invalid = 0,
        Connecting,
        Connected,
        Closed,
        Failed,
    }

    public class SocketClient
    {
        private const int MAX_READ = 8192;

        /// <summary>
        /// 连接上
        /// </summary>
        public Action OnConnected = null;

        /// <summary>
        /// 连接失败
        /// </summary>
        public Action OnConnectFailed = null;

        /// <summary>
        /// 连接关闭
        /// </summary>
        public Action OnConnectClosed = null;


        private TcpClient client = null;
        private NetworkStream outStream = null;
        private MemoryStream memStream;
        private BinaryReader reader;

        private byte[] byteBuffer = new byte[MAX_READ];

        private string mHost = null;
        private int mPort;
        private SocketState socketState = SocketState.Invalid;

        public SocketClient() { }


        public string Host
        {
            get { return this.mHost; }
        }

        public int Port
        {
            get { return this.mPort; }
        }

        public bool Connected
        {
            get { return client != null && client.Connected; }
        }

        public void Connect(string host, int port)
        {
            if (this.socketState == SocketState.Connecting)
            {
                Debug.LogWarning("Socket is connecting.");
                return;
            }

            if (client != null && client.Connected)
            {
                Debug.LogWarning("Socket is connected.");
                return;
            }

            this.mHost = host;
            this.mPort = port;

            client = null;
            client = new TcpClient();
            client.SendTimeout = 1000;
            client.ReceiveTimeout = 1000;
            client.NoDelay = true;
            try
            {
                this.socketState = SocketState.Connecting;
                client.BeginConnect(host, port, new AsyncCallback(OnConnect), null);
            }
            catch (Exception e)
            {
                ErrorClose();
                Debug.LogError(e.Message);
            }
        }

        /// <summary>
        /// 连接上服务器
        /// </summary>
        private void OnConnect(IAsyncResult asr)
        {
            if (!client.Connected)
            {
                this.socketState = SocketState.Failed;
                DispatchEvent(SocketState.Failed);
            }
            else
            {
                this.socketState = SocketState.Connected;

                memStream = new MemoryStream();
                reader = new BinaryReader(memStream);

                outStream = client.GetStream();
                client.GetStream().BeginRead(byteBuffer, 0, MAX_READ, new AsyncCallback(OnReceive), null);

                DispatchEvent(SocketState.Connected);
            }
        }

        private void DispatchEvent(SocketState state)
        {
            if (state == SocketState.Connected)
            {
                if (OnConnected != null)
                {
                    OnConnected.Invoke();
                }
            }
            else if (state == SocketState.Failed)
            {
                if (OnConnectFailed != null)
                {
                    OnConnectFailed.Invoke();
                }
            }
            else if (state == SocketState.Closed)
            {
                if (OnConnectClosed != null)
                {
                    OnConnectClosed.Invoke();
                }
            }
        }

        /// <summary>
        /// 关闭链接，提供外部关闭
        /// </summary>
        public void Close()
        {
            if (client != null)
            {
                if (client.Connected) client.Close();
                client = null;
            }
            this.socketState = SocketState.Closed;
        }

        /// <summary>
        /// 内部错误关闭，比如链接断开
        /// </summary>
        private void ErrorClose()
        {
            Close();
            DispatchEvent(SocketState.Closed);
        }



        /// <summary>
        /// 发送数据
        /// </summary>
        public void Send(byte[] message)
        {
            if (client != null)
            {
                MemoryStream ms = null;
                using (ms = new MemoryStream())
                {
                    ms.Position = 0;
                    BinaryWriter writer = new BinaryWriter(ms);
                    ushort msglen = (ushort)message.Length;
                    writer.Write(msglen);
                    writer.Write(message);
                    writer.Flush();

                    if (client.Connected)
                    {
                        byte[] payload = ms.ToArray();
                        outStream.BeginWrite(payload, 0, payload.Length, new AsyncCallback(OnWrite), null);
                    }
                    else
                    {
                        ErrorClose();
                        Debug.LogError("The client.connected --> false");
                    }
                }
            }
        }

        /// <summary>
        /// 向链接写入数据流
        /// </summary>
        private void OnWrite(IAsyncResult r)
        {
            try
            {
                outStream.EndWrite(r);
            }
            catch (Exception ex)
            {
                Debug.LogError("OnWrite --> " + ex.Message);
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        private void OnReceive(IAsyncResult asr)
        {
            int bytesRead = 0;
            try
            {
                lock (client.GetStream())
                {
                    //读取字节流到缓冲区
                    bytesRead = client.GetStream().EndRead(asr);
                }

                if (bytesRead > 0)
                {
                    OnReceive(byteBuffer, bytesRead);   //分析数据包内容，抛给逻辑层
                }

                lock (client.GetStream())
                {
                    //分析完，再次监听服务器发过来的新消息
                    Array.Clear(byteBuffer, 0, byteBuffer.Length);   //清空数组
                    client.GetStream().BeginRead(byteBuffer, 0, MAX_READ, new AsyncCallback(OnReceive), null);
                }
            }
            catch (Exception e)
            {
                ErrorClose();
                Debug.LogError(e.Message);
            }
        }

        /// <summary>
        /// 接收到消息
        /// </summary>
        private void OnReceive(byte[] bytes, int length)
        {
            memStream.Seek(0, SeekOrigin.End);
            memStream.Write(bytes, 0, length);
            //Reset to beginning
            memStream.Seek(0, SeekOrigin.Begin);
            while (RemainingBytes() > 2)
            {
                ushort messageLen = reader.ReadUInt16();
                if (RemainingBytes() >= messageLen)
                {
                    MemoryStream ms = new MemoryStream();
                    BinaryWriter writer = new BinaryWriter(ms);
                    writer.Write(reader.ReadBytes(messageLen));
                    ms.Seek(0, SeekOrigin.Begin);
                    OnReceivedMessage(ms);
                }
                else
                {
                    memStream.Position = memStream.Position - 2;
                    break;
                }
            }
            //Create a new stream with any leftover bytes
            byte[] leftover = reader.ReadBytes((int)RemainingBytes());
            memStream.SetLength(0);
            memStream.Write(leftover, 0, leftover.Length);
        }

        /// <summary>
        /// 剩余的字节
        /// </summary>
        private long RemainingBytes()
        {
            return memStream.Length - memStream.Position;
        }

        /// <summary>
        /// 接收到消息
        /// </summary>
        /// <param name="ms"></param>
        private void OnReceivedMessage(MemoryStream ms)
        {
            //BinaryReader r = new BinaryReader(ms);
            //byte[] message = r.ReadBytes((int)(ms.Length - ms.Position));
            ////int msglen = message.Length;

            //ByteBuffer buffer = new ByteBuffer(message);
            //int mainId = buffer.ReadShort();
            //NetworkManager.AddEvent(mainId, buffer);
        }

    }


}
