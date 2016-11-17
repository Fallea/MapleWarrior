using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CardPanel : UIViewPanel
{

    public GameObject cardItemPrefab;
    public Scrollbar scrollbar;

    private PanelItemPool<CardItem> pool = new PanelItemPool<CardItem>();

    private List<CardItem> items = new List<CardItem>();


    protected override void Start()
    {
        base.Start();
        pool.SetPrefab(cardItemPrefab);
        started = true;
        ShowCards();
        scrollbar.onValueChanged.AddListener(OnScrollValueChanged);
    }

    public override void Open()
    {
        base.Open();
        ShowCards();
    }

    public override void Close()
    {
        base.Close();
        Clear();
    }

    private void ShowCards()
    {
        if (!started)
        {
            return;
        }
        Clear();
        List<Card> list = DataCacheManager.Instance.cards;
        for (int i = 0; i < list.Count; i++)
        {
            CardItem item = pool.Get();
            items.Add(item);
            item.onItemClick = OnItemClick;
            item.data = list[i];
        }
    }

    private void Clear()
    {
        for (int i = 0; i < items.Count; i++)
        {
            pool.Recovery(items[i]);
        }
        items.Clear();
    }

    public void OnScrollValueChanged(float value)
    {

    }

    private void OnItemClick(UIViewItem item)
    {
        UIManager.Instance.Open(UIPanelUtil.CardDetailPanel, item.data);
    }

}
