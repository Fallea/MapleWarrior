using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapPanel : UIViewPanel
{

    public Text nameTxt;
    public GameObject exploreBtn;


    protected override void Start()
    {
        base.Start();
        ButtonClickListener.Get(exploreBtn).onClick = OnExploreBtnClick;
    }

    public override void Open(object param)
    {
        base.Open(param);
        Display((MapConfigData)param);
    }

    private void Display(MapConfigData mapConfigData)
    {
        nameTxt.text = mapConfigData.name;
    }

    private void OnExploreBtnClick(GameObject go)
    {

    }

}
