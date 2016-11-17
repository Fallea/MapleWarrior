using UnityEngine;
using System.Collections;

/*******************************************************************************
 * 
 *             类名: UIViewPanel
 *             功能: UI面板类
 *             作者: HGQ
 *             日期: 2016.4.22
 *             修改:
 *             
 * *****************************************************************************/

public class UIViewPanel : MonoBehaviour
{

    private UIViewPanelType mPanelType = UIViewPanelType.None;

    /// <summary>
    /// 关闭按钮，可选
    /// </summary>
    public GameObject closeBtn;
    /// <summary>
    /// 面板类型
    /// </summary>
    public UIViewPanelType panelType
    {
        set { mPanelType = value; }
        get { return mPanelType; }
    }

	protected bool started;

    protected virtual void Start()
    {
        if (closeBtn != null)
        {
            ButtonClickListener.Get(closeBtn).onClick = OnCloseBtnClick;
        }
    }

    public virtual void Open()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void Open(object param)
    {
        this.gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        this.gameObject.SetActive(false);
    }

    public virtual bool IsOpen()
    {
        return this.gameObject.activeSelf;
    }

    protected virtual void OnCloseBtnClick(GameObject go)
    {
        if (IsOpen())
        {
            UIManager.Instance.Close(this);
        }
    }
}
