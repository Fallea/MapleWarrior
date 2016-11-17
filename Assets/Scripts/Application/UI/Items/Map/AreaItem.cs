using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaItem : UIViewItem
{
    public GameObject areaMapItem;
    public GameObject[] positions;

    private List<AreaMapItem> items = new List<AreaMapItem>();

    protected override void OnUpdateData()
    {
        AreaConfigData areaConfigData = (AreaConfigData)data;

        if (items.Count < 1)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject go = UIUtils.Duplicate(areaMapItem, positions[i]);
                go.transform.localPosition = Vector3.zero;
                AreaMapItem item = go.GetComponent<AreaMapItem>();
                item.onClick = OnItemClick;
                items.Add(item);
            }

            List<MapConfigData> list = DataCacheManager.Instance.GetMapsByArea(areaConfigData.id);
            for (int i = 0; i < list.Count; i++)
            {
                MapConfigData mapConfigData = list[i];
                if (mapConfigData.slot < items.Count)
                {
                    items[mapConfigData.slot].data = mapConfigData;
                }
            }
        }
    }

    private void OnItemClick(UIViewItem viewItem)
    {
        MapConfigData mapConfigData = (MapConfigData)viewItem.data;
        if (mapConfigData.lastId == 0 || DataCacheManager.Instance.GetMap(mapConfigData.lastId) != null)
        {
            UIManager.Instance.Open(UIPanelUtil.MapPanel, viewItem.data);
        }
        else
        {
            UIManager.Instance.MsgBox("未开启", MsgBoxType.Ok);
        }
    }

}
