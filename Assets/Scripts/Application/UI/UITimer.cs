using UnityEngine;
using System.Collections;

/// <summary>
/// 定时器
/// </summary>
public class UITimer : MonoBehaviour
{
    /// <summary>
    /// 创建定时器
    /// </summary>
    /// <param name="time"></param>
    /// <param name="action"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static UITimer Create(float time, System.Action action, string name = null)
    {
        string timerName = name;
        if (string.IsNullOrEmpty(timerName))
        {
            timerName = "UITimer";
        }
        GameObject go = new GameObject(timerName);
        UITimer timer = go.AddComponent<UITimer>();
        timer.Init(time, action);
        return timer;
    }

    private bool started;
    private float time;

    private System.Action action;
    private float overTime;

    void Update()
    {
        if (started)
        {
            time += Time.deltaTime;
            if (time > overTime)
            {
                started = false;
                if (action != null)
                {
                    action.Invoke();
                    action = null;
                }
                GameObject.Destroy(this.gameObject);
            }
        }
    }

    private void Init(float overTime, System.Action action)
    {
        this.action = action;
        this.overTime = overTime;
        started = true;
    }
}
