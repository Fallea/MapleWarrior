using UnityEngine;
using System.Collections;

/*******************************************************************************
 * 
 *             类名: UIViewItem
 *             功能: UI显示项
 *             作者: HGQ
 *             日期: 2016.4.22
 *             修改:
 *             
 * *****************************************************************************/

public class UIViewItem : MonoBehaviour
{

    public delegate void ViewItemDelegate(UIViewItem item);

    private string mId;
    private int mIndex;
    private object mData;
    private bool mSeleted;

    protected bool started;

    /// <summary>
    /// 字符串ID，用于存储字符串标示
    /// </summary>
    public string id
    {
        get { return this.mId; }
        set { this.mId = value; }
    }

    /// <summary>
    /// int索引，用于存储int标示
    /// </summary>
    public int index
    {
        get { return this.mIndex; }
        set { this.mIndex = value; }
    }

    public object data
    {
        get { return this.mData; }
        set
        {
            this.mData = value;
            this.UpdateData();
        }
    }

    public bool selected
    {
        get { return this.mSeleted; }
        set { this.mSeleted = value; this.UpdateSelected(); }
    }

    //================================================================

    private void UpdateData()
    {
        if (!this.started) { return; }
        if (this.data == null) { return; }
        OnUpdateData();
    }

    protected virtual void OnStart()
    {
        //do something...
    }

    protected virtual void OnUpdateData()
    {
        //do something...
    }

    protected virtual void UpdateSelected()
    {
        //do something...
    }

    //================================================================

    protected virtual void Start()
    {
        this.OnStart();
        this.started = true;
        this.UpdateData();
        this.UpdateSelected();
    }

    //================================================================

    public virtual void Clear()
    {
        this.mData = null;
    }
}
