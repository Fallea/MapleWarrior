
/// <summary>
/// 数据发送到服务端处理
/// </summary>
public class NetSender : TSingleton<NetSender>
{
    private NetSender() { }

    public void SendLogin()
    {

    }

	public void SendPing()
	{
		PingRequest request = new PingRequest (C2SCMD.Ping);
		NetManager.Send(request);
	}

    /// <summary>
    /// 请求数据
    /// </summary>
    public void SendGetRegiment()
    {
        GetRegimentRequest request = new GetRegimentRequest(C2SCMD.GetRegiment);
        request.sessionId = 10000;
        NetManager.Send(request);
    }

    /// <summary>
    /// 创建勇士团的名称
    /// </summary>
    /// <param name="name"></param>
    public void SendCreateRegiment(string name)
    {
        CreateRegimentRequest request = new CreateRegimentRequest(C2SCMD.CreateRegiment);
        request.name = name;
        NetManager.Send(request);
    }

    /// <summary>
    /// 卡牌抽奖
    /// </summary>
    /// <param name="type"></param>
    public void SendCardLottery(int type = 0)
    {
        CardLotteryRequest request = new CardLotteryRequest(C2SCMD.CardLottery);
        request.type = type;
        NetManager.Send(request);
    }
}
