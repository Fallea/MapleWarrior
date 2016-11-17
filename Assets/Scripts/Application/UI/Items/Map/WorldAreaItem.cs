using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// WorldMapPanel界面中的AreaItem项
/// </summary>
public class WorldAreaItem : UIViewItem
{
    public Image icon;
    public Text nameTxt;

    public ViewItemDelegate onClick;

    protected override void OnUpdateData()
    {
        AreaConfigData areaConfigData = (AreaConfigData)data;
        this.nameTxt.text = areaConfigData.name;
        ButtonClickListener.Get(icon.gameObject).onClick = OnClick;
    }

    private void OnClick(GameObject go)
    {
        if (onClick != null)
        {
            onClick.Invoke(this);
        }
    }
}
