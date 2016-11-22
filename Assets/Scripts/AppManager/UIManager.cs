using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*******************************************************************************
 * 
 *             类名: UIManager
 *             功能: UI管理
 *             作者: HGQ
 *             日期: 2016.4.24
 *             修改:
 *             
 * *****************************************************************************/

public class UIManager : TSingleton<UIManager>
{
    private UIManager() { }

    /// <summary>
    /// 存放UI界面跟路径的
    /// </summary>
    public UIViewRoot uiRoot;

    private Dictionary<string, UIViewPanelType> types = new Dictionary<string, UIViewPanelType>();
    private Dictionary<string, UIViewPanel> panels = new Dictionary<string, UIViewPanel>();

    private UIViewPanel mainPanel = null;
    private UIViewPanel loadingPanel = null;

    private UIViewPanel fristOpenPanel = null;
    private UIViewPanel secondOpenPanel = null;
    private UIViewPanel thirdOpenPanel = null;
    private UIViewPanel tipsOpenPanel = null;
    private UIViewPanel noticeOpenPanel = null;
    private UIViewPanel promptOpenPanel = null;


    public void Initialize()
    {
        AddPanelInfo(UIPanelUtil.WelcomePanel, UIViewPanelType.Frist);
        AddPanelInfo(UIPanelUtil.LoadingPanel, UIViewPanelType.Mask);
        AddPanelInfo(UIPanelUtil.CreateRegimentPanel, UIViewPanelType.Frist);
        AddPanelInfo(UIPanelUtil.MainPanel, UIViewPanelType.Main);
        AddPanelInfo(UIPanelUtil.MessageBoxPanel, UIViewPanelType.Prompt);
        AddPanelInfo(UIPanelUtil.CardPanel, UIViewPanelType.Frist);
        AddPanelInfo(UIPanelUtil.CardDetailPanel, UIViewPanelType.Second);
        AddPanelInfo(UIPanelUtil.CardLotteryPanel, UIViewPanelType.Frist);
        AddPanelInfo(UIPanelUtil.WorldMapPanel, UIViewPanelType.Frist);
        AddPanelInfo(UIPanelUtil.AreaMapPanel, UIViewPanelType.Second);
        AddPanelInfo(UIPanelUtil.ExplorePanel, UIViewPanelType.Second);
        AddPanelInfo(UIPanelUtil.MapPanel, UIViewPanelType.Third);
        AddPanelInfo(UIPanelUtil.SkillDetailPanel, UIViewPanelType.Third);
    }

    private void AddPanelInfo(string name, UIViewPanelType type)
    {
        if (types.ContainsKey(name))
        {
            Debug.LogError(">> UIManager > AddPanelInfo > repeat name = " + name);
            return;
        }
        types.Add(name, type);
    }

    /// <summary>
    /// 面板自己注册使用的，预设创建在场景中的面板
    /// </summary>
    /// <param name="name"></param>
    /// <param name="panel"></param>
    public void Register(string name, UIViewPanel panel)
    {
        if (panels.ContainsKey(name))
        {
            return;
        }
        if (types.ContainsKey(name))
        {
            panel.panelType = types[name];
        }
        panels.Add(name, panel);
    }

    //================================================================

    private string panelNameAtCloseLoading = null;
    private float delayTimeAtCloseLoading = 0;

    /// <summary>
    /// 设置Loading面板，由于是预先设置在启动场景中的
    /// </summary>
    /// <param name="panel"></param>
    public void SetLoadingPanel(UIViewPanel panel)
    {
        loadingPanel = panel;
    }

    public void OpenLoadingPanel()
    {
        loadingPanel.Open();
    }

    public void CloseLoadingPanel()
    {
        loadingPanel.Close();
    }

    public void SetCloseLoadingByPanel(string panelName, float delayTime = 0)
    {
        panelNameAtCloseLoading = panelName;
        delayTimeAtCloseLoading = delayTime;
    }

    //================================================================

    /// <summary>
    /// 由于主面板有别于其他面板需单独处理，开启时会把统一管理的3层面板给关闭
    /// </summary>
    public void OpenMainPanel()
    {
        if (mainPanel == null)
        {
            LoadPanel(UIPanelUtil.MainPanel);
        }
        else
        {
            HandleOpen(UIPanelUtil.MainPanel, mainPanel);
        }
    }

    //================================================================

    /// <summary>
    /// 打开统一管理的面板
    /// </summary>
    /// <param name="name"></param>
    public void Open(string name, object obj = null)
    {
        if (panels.ContainsKey(name))
        {
            HandleOpen(name, panels[name], obj);
        }
        else
        {
            LoadPanel(name, obj);
        }
    }

    /// <summary>
    /// 处理面板开启，同时关闭根据面板类型相关的面板
    /// </summary>
    /// <param name="name"></param>
    private void HandleOpen(string name, UIViewPanel panel, object param = null)
    {
        if (panel.panelType == UIViewPanelType.Frist)
        {
            CloseThirdPanel();
            CloseSecondPanel();
            if (panel != fristOpenPanel)
            {
                CloseFristPanel();
            }
            fristOpenPanel = panel;
            panel.transform.SetParent(uiRoot.FristRoot.transform);
        }
        else if (panel.panelType == UIViewPanelType.Second)
        {
            CloseThirdPanel();
            if (panel != secondOpenPanel)
            {
                CloseSecondPanel();
            }
            secondOpenPanel = panel;
            panel.transform.SetParent(uiRoot.SecondRoot.transform);
        }
        else if (panel.panelType == UIViewPanelType.Third)
        {
            if (panel != thirdOpenPanel)
            {
                CloseThirdPanel();
            }
            thirdOpenPanel = panel;
            panel.transform.SetParent(uiRoot.ThirdRoot.transform);
        }
        else if (panel.panelType == UIViewPanelType.Tips)
        {
            if (panel != tipsOpenPanel)
            {
                CloseTipsPanel();
            }
            tipsOpenPanel = panel;
            panel.transform.SetParent(uiRoot.TipsRoot.transform);
        }
        else if (panel.panelType == UIViewPanelType.Notice)
        {
            if (panel != noticeOpenPanel)
            {
                CloseNoticePanel();
            }
            noticeOpenPanel = panel;
            panel.transform.SetParent(uiRoot.NoticeRoot.transform);
        }
        else if (panel.panelType == UIViewPanelType.Prompt)
        {
            if (panel != promptOpenPanel)
            {
                ClosePromptPanel();
            }
            promptOpenPanel = panel;
            panel.transform.SetParent(uiRoot.PromptRoot.transform);
        }
        else if (panel.panelType == UIViewPanelType.Main)
        {
            mainPanel = panel;
            panel.transform.SetParent(uiRoot.MainRoot.transform);
            UIManager.Instance.CloseAll();
        }

        if (param != null)
        {
            panel.Open(param);
        }
        else
        {
            panel.Open();
        }

        // Check close LoadingPanel
        if (name.Equals(panelNameAtCloseLoading))
        {
            panelNameAtCloseLoading = null;
            if (delayTimeAtCloseLoading > 0)
            {
                UITimer.Create(delayTimeAtCloseLoading, CloseLoadingPanel, "CloseLoadingTimer");
            }
            else
            {
                CloseLoadingPanel();
            }
            delayTimeAtCloseLoading = 0;
        }
    }

    private void CloseThirdPanel()
    {
        if (thirdOpenPanel != null && thirdOpenPanel.IsOpen())
        {
            thirdOpenPanel.Close();
        }
        thirdOpenPanel = null;
    }

    private void CloseSecondPanel()
    {
        if (secondOpenPanel != null && secondOpenPanel.IsOpen())
        {
            secondOpenPanel.Close();
        }
        secondOpenPanel = null;
    }

    private void CloseFristPanel()
    {
        if (fristOpenPanel != null && fristOpenPanel.IsOpen())
        {
            fristOpenPanel.Close();
        }
        fristOpenPanel = null;
    }

    private void CloseTipsPanel()
    {
        if (tipsOpenPanel != null && tipsOpenPanel.IsOpen())
        {
            tipsOpenPanel.Close();
        }
        tipsOpenPanel = null;
    }

    private void CloseNoticePanel()
    {
        if (noticeOpenPanel != null && noticeOpenPanel.IsOpen())
        {
            noticeOpenPanel.Close();
        }
        noticeOpenPanel = null;
    }

    private void ClosePromptPanel()
    {
        if (promptOpenPanel != null && promptOpenPanel.IsOpen())
        {
            promptOpenPanel.Close();
        }
        promptOpenPanel = null;
    }

    /// <summary>
    /// 关闭面板
    /// </summary>
    /// <param name="name"></param>
    public void Close(string name)
    {
        if (panels.ContainsKey(name))
        {
            UIViewPanel panel = panels[name];
            if (panel.IsOpen())
            {
                Close(panel);
            }
        }
    }

    /// <summary>
    /// 关闭面板，会根据面板类型处理
    /// </summary>
    /// <param name="name"></param>
    public void Close(UIViewPanel panel)
    {
        if (panel.panelType == UIViewPanelType.Frist)
        {
            CloseThirdPanel();
            CloseSecondPanel();
            CloseFristPanel();
        }
        else if (panel.panelType == UIViewPanelType.Second)
        {
            CloseThirdPanel();
            CloseSecondPanel();
        }
        else if (panel.panelType == UIViewPanelType.Third)
        {
            CloseThirdPanel();
        }
        else if (panel.panelType == UIViewPanelType.Tips)
        {
            CloseTipsPanel();
        }
        else if (panel.panelType == UIViewPanelType.Notice)
        {
            CloseNoticePanel();
        }
        else if (panel.panelType == UIViewPanelType.Prompt)
        {
            ClosePromptPanel();
        }
    }

    /// <summary>
    /// 关闭所有面板，但只处理统一3个层级的面板
    /// </summary>
    public void CloseAll()
    {
        if (fristOpenPanel != null)
        {
            Close(fristOpenPanel);
        }
    }

    //================================================================

    /// <summary>
    /// 加载面板，并设置面板类型，设置面板位置、对齐方式等
    /// </summary>
    /// <param name="name"></param>
    private void LoadPanel(string name, object param = null)
    {
        ResourceLoadManager.Instance.LoadAssetAsync<GameObject>(string.Format("UI/Panels/{0}", name), (obj) =>
        {
            if (panels.ContainsKey(name))
            {
                return;
            }

            GameObject go = GameObject.Instantiate(obj);
            go.name = name;
            go.transform.SetParent(uiRoot.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = new Vector3(1, 1, 1);

            RectTransform rt = go.GetComponent<RectTransform>();
            rt.sizeDelta = Vector2.zero;

            UIViewPanel panel = go.GetComponent<UIViewPanel>();
            if (types.ContainsKey(name))
            {
                panel.panelType = types[name];
                panels.Add(name, panel);
                HandleOpen(name, panel, param);
            }
            else
            {
                go.SetActive(false);
                Debug.LogError(">> UIManager > LoadPanel > not set type = " + name);
            }
        });
    }

    //================================================================

    /// <summary>
    /// 提示确认框
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="callback"></param>
    /// <param name="type"></param>
    public void MsgBox(string msg, MsgBoxType type = MsgBoxType.Ok, System.Action<MsgBoxEvent, object> callback = null, object param = null)
    {
        MsgBoxParamData data = new MsgBoxParamData();
        data.msg = msg;
        data.type = type;
        data.callback = callback;
        data.param = param;
        Open(UIPanelUtil.MessageBoxPanel, data);
    }

    //================================================================

    private bool mLoaded = false;
    private List<string> atlases = new List<string>();
    private int loadAtlasCount = 0;

    private UISpriteAtlas cardAtlas;
    private UISpriteAtlas skillAtlas;
    private UISpriteAtlas headAtlas;

    public void Load()
    {
        //================================

        atlases.Add("CardAtlas");
        atlases.Add("SkillAtlas");
        atlases.Add("HeadAtlas");

        //================================
        loadAtlasCount = 0;
        LoadAtlas(atlases[0]);
    }
    private void LoadAtlas(string name)
    {
        ResourceLoadManager.Instance.LoadAssetAsync<GameObject>(string.Format("UI/{0}", name), AtlasLoadCompleted);
    }

    private void AtlasLoadCompleted(GameObject go)
    {
        if (atlases[loadAtlasCount] == "CardAtlas")
        {
            this.cardAtlas = go.GetComponent<UISpriteAtlas>();
            this.cardAtlas.Init();
        }
        else if (atlases[loadAtlasCount] == "SkillAtlas")
        {
            this.skillAtlas = go.GetComponent<UISpriteAtlas>();
            this.skillAtlas.Init();
        }
        else if (atlases[loadAtlasCount] == "HeadAtlas")
        {
            this.headAtlas = go.GetComponent<UISpriteAtlas>();
            this.headAtlas.Init();
        }


        loadAtlasCount++;
        if (loadAtlasCount >= atlases.Count)
        {
            this.mLoaded = true;
            atlases.Clear();
        }
        else
        {
            LoadAtlas(atlases[loadAtlasCount]);
        }
    }

    /// <summary>
    /// UI方面加载完成
    /// </summary>
    public bool loaded
    {
        get { return this.mLoaded; }
    }

    /// <summary>
    /// 获取卡牌的图片
    /// </summary>
    public Sprite GetCardSprite(string name)
    {
        if (cardAtlas == null)
        {
            Debug.LogError(">> CardAtlas is null.");
            return null;
        }
        return cardAtlas.GetSprite(name);
    }

    /// <summary>
    /// 获取技能的图片
    /// </summary>
    public Sprite GetSkillSprite(string name)
    {
        if (skillAtlas == null)
        {
            Debug.LogError(">> SkillAtlas is null.");
            return null;
        }
        return skillAtlas.GetSprite(name);
    }

}
