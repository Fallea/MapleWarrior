using UnityEngine;
using System.Collections;

public class WelcomePanel : UIViewPanel
{

    public GameObject background;

    private bool isBackgroundClick = false;

    protected override void Start()
    {
        base.Start();
        ButtonClickListener.Get(background).onClick += OnBackgroundClick;
        UIManager.Instance.Register(UIPanelUtil.WelcomePanel, this);
        UIManager.Instance.Open(UIPanelUtil.WelcomePanel);
    }

    private void OnBackgroundClick(GameObject go)
    {
        UIManager.Instance.OpenLoadingPanel();
        isBackgroundClick = true;
    }

    void Update()
    {
        if (isBackgroundClick && ConfigManager.Instance.loaded && UIManager.Instance.loaded)
        {
            isBackgroundClick = false;
            DataCenterManager.Instance.GetRegiment();
        }
    }
}
