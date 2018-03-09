using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 完整的Color值Tween，针对Text，Image，RawImage等实现了MaskableGraphic类的对象，并只处理对象上绑定的脚本
/// </summary>
public class TweenColor : UITweener
{
    public Color from = Color.white;
    public Color to = Color.white;

    bool mCached = false;

    MaskableGraphic graphic;

    void Cache()
    {
        mCached = true;

        graphic = GetComponent<MaskableGraphic>();
    }

    /// <summary>
    /// Tween's current value.
    /// </summary>
    public Color value
    {
        get
        {
            if (!mCached) Cache();
            if (graphic != null) return graphic.color;
            return Color.black;
        }
        set
        {
            if (!mCached) Cache();

            if (graphic != null)
            {
                graphic.color = value;
            }
        }
    }

    /// <summary>
    /// Tween the value.
    /// </summary>
    protected override void OnUpdate(float factor, bool isFinished) { value = Color.Lerp(from, to, factor); }

    /// <summary>
    /// Start the tweening operation.
    /// </summary>
    static public TweenColor Begin(GameObject go, float duration, Color color)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying) return null;
#endif
        TweenColor comp = UITweener.Begin<TweenColor>(go, duration);
        comp.from = comp.value;
        comp.to = color;

        if (duration <= 0f)
        {
            comp.Sample(1f, true);
            comp.enabled = false;
        }
        return comp;
    }

    [ContextMenu("Set 'From' to current value")]
    public override void SetStartToCurrentValue() { from = value; }

    [ContextMenu("Set 'To' to current value")]
    public override void SetEndToCurrentValue() { to = value; }

    [ContextMenu("Assume value of 'From'")]
    void SetCurrentValueToStart() { value = from; }

    [ContextMenu("Assume value of 'To'")]
    void SetCurrentValueToEnd() { value = to; }
}
