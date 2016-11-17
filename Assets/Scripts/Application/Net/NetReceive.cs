using System.Collections.Generic;

public class NetReceive : TSingleton<NetReceive>
{
    private NetReceive() { }

    public delegate void NetReceiveDelegate(NetResponse response);

    private Dictionary<S2CCMD, NetReceiveDelegate> delegates = new Dictionary<S2CCMD, NetReceiveDelegate>();



    //================================================================

    /// <summary>
    /// 同一个实例中的同一方法添加多次，调用也会调用多次
    /// </summary>
    public void Add(S2CCMD protocol, NetReceiveDelegate receiveDelegate)
    {
        if (delegates.ContainsKey(protocol))
        {
            delegates[protocol] += receiveDelegate;
        }
        else
        {
            delegates[protocol] = receiveDelegate;
        }
    }

    public void Remove(S2CCMD protocol, NetReceiveDelegate receiveDelegate)
    {
        if (delegates.ContainsKey(protocol))
        {
            delegates[protocol] -= receiveDelegate;
        }
    }

    public void RemoveAll(S2CCMD protocol)
    {
        if (delegates.ContainsKey(protocol))
        {
            delegates[protocol] = null;
        }
    }

    public void Receive(NetResponse response)
    {
        if (delegates.ContainsKey(response.cmd))
        {
            NetReceiveDelegate callback = delegates[response.cmd];
            if (callback != null)
            {
                callback.Invoke(response);
            }
        }
    }

    //================================================================



}