using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainPanel : UIViewPanel
{

    public GameObject testBtn;
    public GameObject cardBtn;
    public GameObject bagBtn;
    public GameObject cardLotteryBtn;
    public GameObject exploreBtn;
    public GameObject worldMapBtn;
    

    public Text playerNameTxt;
    public Text glodTxt;
    public Text powerTxt;
    public Text expTxt;
    public Text cutTimeTxt;

    protected override void Start()
    {
        base.Start();
        ButtonClickListener.Get(testBtn).onClick = OnTestBtnClick;
        ButtonClickListener.Get(cardBtn).onClick = OnCardBtnClick;
        ButtonClickListener.Get(cardLotteryBtn).onClick = OnCardLotteryBtnClick;
        ButtonClickListener.Get(exploreBtn).onClick = OnExploreBtnClick;
        ButtonClickListener.Get(worldMapBtn).onClick = OnWorldMapBtnClick;
        ButtonClickListener.Get(bagBtn).onClick = OnBagBtnClick;

        OnUpdatePlayer();
        DelegateManager.Instance.Add(DelegateCommand.UpdatePlayer, OnUpdatePlayer);
        DelegateManager.Instance.Add(DelegateCommand.UpdatePower, OnUpdatePower);
    }

    void Update()
    {
        if (DataCacheManager.Instance.player.power < DataCacheManager.Instance.player.powerMax)
        {
            long temp = DataCacheManager.Instance.serverTime - DataCacheManager.Instance.player.lastPowerTime;
            if (temp < AppConfig.POWER_TIME)
            {
                int second = (AppConfig.POWER_TIME - (int)temp) / 1000;
                int minute = second / 60;
                cutTimeTxt.text = string.Format("{0:00}:{1:00}", minute, second % 60);
            }
            else
            {
                cutTimeTxt.text = "00:00";
            }
        }
        else
        {
            cutTimeTxt.text = "";
        }
    }

    private void OnTestBtnClick(GameObject go)
    {

    }

    private void OnCardBtnClick(GameObject go)
    {
        UIManager.Instance.Open(UIPanelUtil.CardPanel);
    }

    private void OnBagBtnClick(GameObject go)
    {
        UIManager.Instance.Open(UIPanelUtil.BagPanel);
    }

    private void OnCardLotteryBtnClick(GameObject go)
    {
        UIManager.Instance.Open(UIPanelUtil.CardLotteryPanel);
    }

    private void OnExploreBtnClick(GameObject go)
    {
        UIManager.Instance.Open(UIPanelUtil.ExplorePanel);
    }

    private void OnWorldMapBtnClick(GameObject go)
    {
        UIManager.Instance.Open(UIPanelUtil.WorldMapPanel);
    }

    private void OnUpdatePlayer()
    {
        playerNameTxt.text = DataCacheManager.Instance.player.name;
        glodTxt.text = DataCacheManager.Instance.player.gold.ToString();
        powerTxt.text = DataCacheManager.Instance.player.power + "/" + DataCacheManager.Instance.player.powerMax;
        expTxt.text = DataCacheManager.Instance.player.exp.ToString();
    }

    private void OnUpdatePower()
    {
        powerTxt.text = DataCacheManager.Instance.player.power + "/" + DataCacheManager.Instance.player.powerMax;
    }
}
