using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataCacheManager : TSingleton<DataCacheManager>
{
    DataCacheManager() { }

    /// <summary>
    /// 服务器时间
    /// </summary>
    public long serverTime;

    private Player mPlayer = new Player();
    /// <summary>
    /// 玩家数据对象，勇士团数据对象
    /// </summary>
    public Player player
    {
        get { return this.mPlayer; }
    }

    public void Cache(SvDataPlayer svDataPlayer)
    {
        mPlayer.Cache(svDataPlayer);
        mCards.Clear();
        for (int i = 0, length = svDataPlayer.cards.Count; i < length; i++)
        {
            mCards.Add(new Card().Cache(svDataPlayer.cards[i]));
        }
        mAreas.Clear();
        mMaps.Clear();
        for (int i = 0, length = svDataPlayer.areas.Count; i < length; i++)
        {
            Area area = new Area().Cache(svDataPlayer.areas[i]);
            mAreas.Add(area);
            for (int j = 0, count = area.maps.Count; j < count; j++)
            {
                mMaps.Add(area.maps[j].id, area.maps[j]);
            }
        }
    }

    //================================================================
    //================================================================

    private List<Card> mCards = new List<Card>();
    /// <summary>
    /// 卡牌数据
    /// </summary>
    public List<Card> cards
    {
        get { return this.mCards; }
    }

    /// <summary>
    /// 添加一张新的卡牌
    /// </summary>
    public Card AddNewCard(SvDataCard svDataCard)
    {
        Card card = new Card().Cache(svDataCard);
        mCards.Add(card);
        return card;
    }

    /// <summary>
    /// 移除卡牌
    /// </summary>
    public Card RemoveCard(long id)
    {
        for (int i = 0, length = mCards.Count; i < length; i++)
        {
            if (mCards[i].id == id)
            {
                Card card = mCards[i];
                mCards.RemoveAt(i);
                return card;
            }
        }
        return null;
    }

    //================================================================
    //================================================================

    private List<Area> mAreas = new List<Area>();
    /// <summary>
    /// 存储已经探索过的地图
    /// </summary>
    private Dictionary<int, Map> mMaps = new Dictionary<int, Map>();


    /// <summary>
    /// 区域地图数据
    /// </summary>
    public List<Area> areas
    {
        get { return this.mAreas; }
    }

    public Area GetArea(int id)
    {
        for (int i = 0; i < mAreas.Count; i++)
        {
            if (mAreas[i].id == id)
            {
                return mAreas[i];
            }
        }
        return null;
    }

    public Map GetMap(int id)
    {
        if (mMaps.ContainsKey(id))
        {
            return mMaps[id];
        }
        return null;
    }

    /// <summary>
    /// 根据区域地图ID获取该区域下的所有地图配置
    /// </summary>
    public List<MapConfigData> GetMapsByArea(int areaId)
    {
        List<MapConfigData> result = new List<MapConfigData>();
        List<MapConfigData> list = ConfigManager.Instance.GetMapConfigList;
        for (int i = 0, length = list.Count; i < length; i++)
        {
            if (list[i].areaId == areaId)
            {
                result.Add(list[i]);
            }
        }
        return result;
    }

}
