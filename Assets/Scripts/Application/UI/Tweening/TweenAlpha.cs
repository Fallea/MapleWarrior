using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 透明值Tween，针对Text，Image，RawImage等实现了MaskableGraphic类的对象，并只处理对象上绑定的脚本
/// </summary>
public class TweenAlpha : UITweener
{
    [Range(0f, 1f)]
    public float from = 1f;
    [Range(0f, 1f)]
    public float to = 1f;
    /// <summary>
    /// 包含子对象
    /// </summary>
    public bool includeChildren = false;

    bool mCached = false;
    bool mChildrenCached = false;

    MaskableGraphic graphic;
    List<MaskableGraphic> childrenGraphic = new List<MaskableGraphic>();


    void Cache()
    {
        mCached = true;

        graphic = GetComponent<MaskableGraphic>();
    }

    void ChildrenCache()
    {
        mChildrenCached = true;
        if (childrenGraphic.Count > 0) childrenGraphic.Clear();

        childrenGraphic.AddRange(this.GetComponentsInChildren<MaskableGraphic>());
    }

    /// <summary>
    /// Tween's current value.
    /// </summary>
    public float value
    {
        get
        {
            if (!mCached) Cache();

            if (graphic != null) return graphic.color.a;
            return 0;
        }
        set
        {
            if (!mCached) Cache();

            Color c;

            if (includeChildren)
            {
                if (!mChildrenCached) ChildrenCache();

                MaskableGraphic temp;
                for (int i = 0, length = childrenGraphic.Count; i < length; i++)
                {
                    temp = childrenGraphic[i];
                    if (temp != null)
                    {
                        c = temp.color;
                        c.a = value;
                        temp.color = c;
                    }
                }
            }
            else
            {
                if (graphic != null)
                {
                    c = graphic.color;
                    c.a = value;
                    graphic.color = c;
                }
            }

        }
    }

    /// <summary>
    /// Tween the value.
    /// </summary>
    protected override void OnUpdate(float factor, bool isFinished) { value = Mathf.Lerp(from, to, factor); }

    /// <summary>
    /// Start the tweening operation.
    /// </summary>
    static public TweenAlpha Begin(GameObject go, float duration, float alpha)
    {
        TweenAlpha comp = UITweener.Begin<TweenAlpha>(go, duration);
        comp.from = comp.value;
        comp.to = alpha;

        if (duration <= 0f)
        {
            comp.Sample(1f, true);
            comp.enabled = false;
        }
        return comp;
    }

    public override void SetStartToCurrentValue() { from = value; }
    public override void SetEndToCurrentValue() { to = value; }

}
