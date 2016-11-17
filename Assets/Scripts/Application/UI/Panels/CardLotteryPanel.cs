using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CardLotteryPanel : UIViewPanel
{

    public GameObject drawBtn;

    public GameObject cardGroup;
    public TweenScale tweenScale;
    public CardLotteryItem item;

    private ButtonClickListener drawBtnListener;

    protected override void Start()
    {
        base.Start();

        drawBtnListener = ButtonClickListener.Get(drawBtn);
        drawBtnListener.onClick = OnDrawBtnClick;
        drawBtnListener.clickInterval = 1;

        ButtonClickListener.Get(item.gameObject).onClick = OnItemClick;

        cardGroup.SetActive(false);
    }

    public override void Close()
    {
        base.Close();
        cardGroup.SetActive(false);
    }

    private void OnDrawBtnClick(GameObject go)
    {
        NetReceive.Instance.Add(S2CCMD.CardLottery, OnCardLottery);
        NetSender.Instance.SendCardLottery();
    }

    private void OnItemClick(GameObject go)
    {
        Debug.Log(1);
    }


    //================================================================

    private void OnCardLottery(NetResponse netResponse)
    {
        drawBtnListener.Reset();
        NetReceive.Instance.Remove(S2CCMD.CardLottery, OnCardLottery);

        if (netResponse.error > 0)
        {
            ErrorTipsManager.Instance.Handle(netResponse.error);
        }
        else
        {
            CardLotteryResponse response = (CardLotteryResponse)netResponse;
            Card card = DataCacheManager.Instance.AddNewCard(response.card);
            item.data = card;
            cardGroup.SetActive(true);
            tweenScale.ResetToBeginning();
            tweenScale.PlayForward();
        }
    }

    
}
