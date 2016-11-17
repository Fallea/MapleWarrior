using UnityEngine;
using System.Collections;

/// <summary>
/// 数据中心，所有与数据相关的处理都放在这里处理
/// </summary>
public class DataCenterManager : TSingleton<DataCenterManager>
{

    DataCenterManager() { }

    public void Initialize()
    {
        NetReceive.Instance.Add(S2CCMD.Ping, OnPing);
        NetReceive.Instance.Add(S2CCMD.PushPower, OnPushPower);
    }

    //================================================================

    public void Ping()
    {
        NetSender.Instance.SendPing();
    }

    private void OnPing(NetResponse netResponse)
    {
        PingResponse response = (PingResponse)netResponse;
        DataCacheManager.Instance.serverTime = response.time;
        DataCacheManager.Instance.player.lastPowerTime = response.lastPowerTime;
        Debug.Log(DataCacheManager.Instance.serverTime);
    }

    //================================================================

    /// <summary>
    /// 获取勇士团的数据
    /// </summary>
    public void GetRegiment()
    {
        NetReceive.Instance.Add(S2CCMD.GetRegiment, OnGetRegiment);
        NetSender.Instance.SendGetRegiment();
    }

    private void OnGetRegiment(NetResponse netResponse)
    {
        NetReceive.Instance.Remove(S2CCMD.GetRegiment, OnGetRegiment);
        GetRegimentResponse response = (GetRegimentResponse)netResponse;
        DataCacheManager.Instance.serverTime = response.serverTime;
        if (response.player != null && response.player.id > 0)
        {
            AppConfig.LEVEL_MAX = response.levelMax;
            AppConfig.CARD_LV_MAX = response.cardLvMax;
            AppConfig.CARD_NUM_MAX = response.cardNumMax;
            AppConfig.POWER_TIME = response.powerTime;

            DataCacheManager.Instance.Cache(response.player);
            UIManager.Instance.SetCloseLoadingByPanel(UIPanelUtil.MainPanel, 0.5f);
            UIManager.Instance.OpenMainPanel();
        }
        else
        {
            UIManager.Instance.SetCloseLoadingByPanel(UIPanelUtil.CreateRegimentPanel, 0.2f);
            UIManager.Instance.Open(UIPanelUtil.CreateRegimentPanel);
        }
    }

    //================================================================

    public void CreateRegiment(string name)
    {
        NetReceive.Instance.Add(S2CCMD.CreateRegiment, OnGetRegiment);
        UIManager.Instance.OpenLoadingPanel();
        NetSender.Instance.SendCreateRegiment(name);
    }

    private void OnCreateRegiment(NetResponse netResponse)
    {
        CreateRegimentResponse response = (CreateRegimentResponse)netResponse;
        if (response.isCreated)
        {
            DataCenterManager.Instance.GetRegiment();//创建成功，重新请求数据
        }
        else
        {
            UIManager.Instance.CloseLoadingPanel();
            Debug.LogWarning(">> OnCreateRegiment > Create Error.");
        }
        NetReceive.Instance.Remove(S2CCMD.CreateRegiment, OnGetRegiment);
    }

    //================================================================

    private void OnPushPower(NetResponse netResponse)
    {
        PushPowerResponse response = (PushPowerResponse)netResponse;
        DataCacheManager.Instance.serverTime = response.serverTime;
        DataCacheManager.Instance.player.lastPowerTime = response.lastPowerTime;
        DataCacheManager.Instance.player.power = response.power;
        DataCacheManager.Instance.player.powerMax = response.powerMax;
        Debug.Log(response.power);
        DelegateManager.Instance.Send(DelegateCommand.UpdatePower);
    }

    //================================================================
}
