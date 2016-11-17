using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldMapPanel : UIViewPanel
{

    public GameObject worldAreaItem;
    public GameObject[] positions;
    private List<WorldAreaItem> items = new List<WorldAreaItem>();

    public override void Open()
    {
        base.Open();
        Display();
    }

    private void Display()
    {
        if (items.Count < 1)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject go = UIUtils.Duplicate(worldAreaItem, positions[i]);
                go.transform.localPosition = Vector3.zero;
                WorldAreaItem item = go.GetComponent<WorldAreaItem>();
                item.onClick = OnItemClick;
                items.Add(item);
            }
            List<AreaConfigData> list = ConfigManager.Instance.GetAreaConfigList;
            for (int i = 0; i < list.Count; i++)
            {
                AreaConfigData areaConfigData = list[i];
                if (areaConfigData.slot < items.Count)
                {
                    items[areaConfigData.slot].data = areaConfigData;
                }
            }
        }

    }

    private void OnItemClick(UIViewItem viewItem)
    {
        AreaConfigData areaConfigData = (AreaConfigData)viewItem.data;

        if (Area.IsOpen(areaConfigData))
        {
            UIManager.Instance.Open(UIPanelUtil.AreaMapPanel, areaConfigData);
        }
        else
        {
            UIManager.Instance.MsgBox("未开启", MsgBoxType.Ok);
        }
    }
}
