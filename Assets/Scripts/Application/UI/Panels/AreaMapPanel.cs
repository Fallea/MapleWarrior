using UnityEngine;
using System.Collections;

public class AreaMapPanel : UIViewPanel
{

    public AreaItem[] areaItems;


    public override void Open(object param)
    {
        base.Open(param);
        Display((AreaConfigData)param);
    }

    public override void Close()
    {
        base.Close();
        for (int i = 0; i < areaItems.Length; i++)
        {
            areaItems[i].gameObject.SetActive(false);
        }
    }

    private void Display(AreaConfigData areaConfigData)
    {
        if (areaConfigData.slot < areaItems.Length)
        {
            areaItems[areaConfigData.slot].gameObject.SetActive(true);
            areaItems[areaConfigData.slot].data = areaConfigData;
        }
    }
}
