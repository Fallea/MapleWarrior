using UnityEngine;
using System.Collections.Generic;

public class ServerProtocolManager : TSingleton<ServerProtocolManager>
{
    ServerProtocolManager() { }

    public delegate void ServerNetReceiveDelegate(SvPlayerSession session, NetRequest request);

    private Dictionary<C2SCMD, ServerNetReceiveDelegate> delegates = new Dictionary<C2SCMD, ServerNetReceiveDelegate>();

    public void Register()
    {
		delegates[C2SCMD.Ping] = OnPing;
        delegates[C2SCMD.GetRegiment] = OnGetRegiment;
        delegates[C2SCMD.CreateRegiment] = OnCreateRegiment;
        delegates[C2SCMD.CardLottery] = OnCardLottery;
    }

    public void HandleReceive(SvPlayerSession session, NetRequest netRequest)
    {
        if (delegates.ContainsKey(netRequest.cmd))
        {
            delegates[netRequest.cmd].Invoke(session, netRequest);
        }
        else
        {
            Debug.LogError(">> ServerLogicManager > HandleReceive > Not register = " + netRequest.cmd.ToString());
        }
    }

    //================================================================

	private void OnPing(SvPlayerSession session, NetRequest netRequest)
	{
		session.HandlePing((PingRequest)netRequest);
	}

    private void OnGetRegiment(SvPlayerSession session, NetRequest netRequest)
    {
        session.HandleGetRegiment((GetRegimentRequest)netRequest);
    }

    private void OnCreateRegiment(SvPlayerSession session, NetRequest netRequest)
    {
        session.HandleCreateRegiment((CreateRegimentRequest)netRequest);
    }

    private void OnCardLottery(SvPlayerSession session, NetRequest netRequest)
    {
        session.HandleCardLottery((CardLotteryRequest)netRequest);
    }
}
