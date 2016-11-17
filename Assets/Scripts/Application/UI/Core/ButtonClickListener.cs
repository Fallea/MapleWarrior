using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Button click listener.
/// </summary>
public class ButtonClickListener : MonoBehaviour, IPointerClickHandler
{

    public delegate void VoidDelegate(GameObject go);

    static public ButtonClickListener Get(GameObject go)
    {
        ButtonClickListener listener = go.GetComponent<ButtonClickListener>();
        if (listener == null) listener = go.AddComponent<ButtonClickListener>();
        return listener;
    }

    public VoidDelegate onClick;
    /// <summary>
    /// 点击间隔时间，控制连续点击
    /// </summary>
    public float clickInterval = 0;
    private float time = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
        {
            if (clickInterval > 0)
            {
                if (time > Time.time)
                {
                    return;
                }
                time = Time.time + clickInterval;
            }
            onClick.Invoke(this.gameObject);
        }
    }

    public void Reset()
    {
        time = 0;
    }
}
