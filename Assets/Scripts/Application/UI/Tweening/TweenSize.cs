using UnityEngine;

/// <summary>
/// Tween the ui's size.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class TweenSize : UITweener
{
    public Vector2 from;
    public Vector2 to;

    bool mCached = false;
    RectTransform rectTransform;

    void Cache()
    {
        mCached = true;

        rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Tween's current value.
    /// </summary>
    public Vector2 value
    {
        get
        {
            if (!mCached) Cache();
            return rectTransform.sizeDelta;
        }
        set
        {
            if (!mCached) Cache();
            rectTransform.sizeDelta = value;
        }
    }

    /// <summary>
    /// Tween the value.
    /// </summary>
    protected override void OnUpdate(float factor, bool isFinished)
    {
        value = from * (1f - factor) + to * factor;
    }

    /// <summary>
    /// Start the tweening operation.
    /// </summary>
    static public TweenSize Begin(RectTransform transform, float duration, Vector2 size)
    {
        TweenSize comp = UITweener.Begin<TweenSize>(transform.gameObject, duration);
        comp.from = transform.sizeDelta;
        comp.to = size;

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
