using UnityEngine;
using System.Collections;

public class Server : TSingleton<Server>
{
    Server() { }

    /// <summary>
    /// 由于单机，省略了Scoket创建会话这一步，服务器启动时，直接创建一个会话
    /// </summary>
    private SvPlayerSession session;
    /// <summary>
    /// 标示服务器是否启动
    /// </summary>
    private bool started;

	/// <summary>
	/// 计时统计
	/// </summary>
	private float timeCount;

	/// <summary>
	/// 服务器当前时间
	/// </summary>
	public long time;

    public void Start()
    {
        ServerDataManager.Instance.LoadPlayerData();
        ServerProtocolManager.Instance.Register();
        session = new SvPlayerSession();
        Debug.Log(">> Server > Start");
		time = Util.GetTime ();
        started = true;
    }

    public void Stop()
    {
        ServerDataManager.Instance.Save();
        Debug.Log(">> Server > Stop");
        started = false;
    }


    public void Pause()
    {

    }

    public void Resume()
    {
		if (started) 
		{
			time = Util.GetTime ();
			timeCount = 0;
			session.Resume();
		}
    }

    public override void Update()
    {
        if (started)
        {
			time += (int)(Time.deltaTime * 1000);

            //其实是使用线程，由于这里不考虑，所以直接用Update来推动
            session.Update();
			 
			timeCount += Time.deltaTime;
			if(timeCount > 1)
			{
				time = Util.GetTime ();
				timeCount -= 1;
				session.UpdateBySecond();
			}
        }
    }

    public void Receive(NetRequest netRequest)
    {
        if (started)
        {
            session.Receive(netRequest);
        }
    }

    public void Response(NetResponse netResponse)
    {
        NetReceive.Instance.Receive(netResponse);
    }

    //================================================================

}
