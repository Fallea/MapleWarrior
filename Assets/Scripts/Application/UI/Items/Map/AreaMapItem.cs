using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// AreaMapPanel面板中的MapItem项
/// </summary>
public class AreaMapItem : UIViewItem
{

    public Image icon;
    public Text nameTxt;

    public ViewItemDelegate onClick;

    protected override void OnUpdateData()
    {
        MapConfigData mapConfigData = (MapConfigData)data;
        this.nameTxt.text = mapConfigData.name;
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
