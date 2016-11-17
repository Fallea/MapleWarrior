
public class NetManager
{
    public static void Send(NetRequest request)
    {
        //由于没有SCOKET，故直接发给服务器
        Server.Instance.Receive(request);
    }
}