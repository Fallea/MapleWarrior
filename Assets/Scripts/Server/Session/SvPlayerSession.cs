using UnityEngine;
using System.Collections;

/// <summary>
/// 服务器玩家会话
/// </summary>
public class SvPlayerSession
{
    public int id;

    private Queue requests = new Queue();
	private Queue responses = new Queue();

    public void Update()
    {
        if (requests.Count > 0)
        {
            ServerProtocolManager.Instance.HandleReceive(this, (NetRequest)requests.Dequeue());
        }
		if (player.updated) 
		{
			ServerDataManager.Instance.Save(player.svDataPlayer);
			player.updated = false;
		}
		if (responses.Count > 0) 
		{
			while(responses.Count > 0)
			{
				Server.Instance.Response((NetResponse)responses.Dequeue());
			}
		}
    }

	/// <summary>
	/// 每秒执行一次
	/// </summary>
	public void UpdateBySecond()
	{
		HandleUpdateBySecond ();
	}

	public void Resume()
	{
		HandleResume ();
	}

	//================================================================

    public void Receive(NetRequest netRequest)
    {
        requests.Enqueue(netRequest);
    }

    //================================================================

    private SvPlayer player = new SvPlayer();

    public SvPlayer GetPlayer()
    {
        return player;
    }

	//================================================================

	/// <summary>
	/// 每秒更新处理
	/// </summary>
	private void HandleUpdateBySecond()
	{
		if (player.svDataPlayer != null) {
			PowerCutTime (true);
		}
	}

	/// <summary>
	/// 暂停恢复处理
	/// </summary>
	private void HandleResume()
	{
		if (player.svDataPlayer != null) {
			PowerCutTime ();
		}
	}

	//================================================================

	public void PowerCutTime(bool isSendPower = false)
	{
		long temp = Server.Instance.time - player.svDataPlayer.lastPowerTime;
		if (temp > ServerConfig.POWER_TIME) {
			int num = (int)(temp / ServerConfig.POWER_TIME);
			player.AddPower(num);
			player.svDataPlayer.lastPowerTime += num * ServerConfig.POWER_TIME;
			
			player.updated = true;
			if(isSendPower)
			{
				SendPushPower();
			}
		}
	}

    //================================================================

	public void HandlePing(PingRequest request)
	{
		if (player.svDataPlayer != null) 
		{
			Server.Instance.time = Util.GetTime ();
			PingResponse response = new PingResponse(S2CCMD.Ping);
			response.time = Server.Instance.time;
			response.lastPowerTime = player.svDataPlayer.lastPowerTime;
			responses.Enqueue (response);
		}
	}

    public void HandleGetRegiment(GetRegimentRequest request)
    {
        id = request.sessionId;
        player.svDataPlayer = ServerDataManager.Instance.GetSvDataPlayer(id);
		PowerCutTime ();
        GetRegimentResponse response = new GetRegimentResponse(S2CCMD.GetRegiment);
        response.player = player.svDataPlayer;
		response.serverTime = Server.Instance.time;
		response.levelMax = ServerConfig.LEVEL_MAX;
		response.cardLvMax = ServerConfig.CARD_LV_MAX;
		response.cardNumMax = ServerConfig.CARD_NUM_MAX;
		response.powerTime = ServerConfig.POWER_TIME;
		responses.Enqueue (response);
    }

    public void HandleCreateRegiment(CreateRegimentRequest request)
    {
        if (player.svDataPlayer != null)
        {
            player.svDataPlayer.id = 10000;
            player.svDataPlayer.name = request.name;
			player.svDataPlayer.power = 10;
			player.svDataPlayer.lastPowerTime = Server.Instance.time;
			player.updated = true;

            CreateRegimentResponse response = new CreateRegimentResponse(S2CCMD.CreateRegiment);
            response.isCreated = true;
			responses.Enqueue (response);
        }
    }

    public void HandleCardLottery(CardLotteryRequest request)
    {
        if (player.svDataPlayer != null)
        {
            CardLotteryResponse response = new CardLotteryResponse(S2CCMD.CardLottery);
            if (player.svDataPlayer.cards.Count < ServerConfig.CARD_NUM_MAX)
            {
                SvDataCard svDataCard = ServerLogicManager.Instance.NewCard();
                player.AddNewCard(svDataCard);
                response.card = svDataCard;
            }
            else
            {
                response.error = ServerError.CARD_MAX;
            }
			responses.Enqueue (response);
        }
    }

	//================================================================

	private void SendPushPower()
	{
		PushPowerResponse response = new PushPowerResponse(S2CCMD.PushPower);
		response.serverTime = Server.Instance.time;
		response.lastPowerTime = player.svDataPlayer.lastPowerTime;
		response.power = player.svDataPlayer.power;
		response.powerMax = player.svDataPlayer.powerMax;
		responses.Enqueue (response);
	}


}
