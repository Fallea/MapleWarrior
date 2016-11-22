using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*******************************************************************************
 * 
 *             类名: AppMainUpdate
 *             功能: 主更新循环类，管理整个运行流程和操作等控制
 *             作者: HGQ
 *             日期: 2016.4.22
 *             修改:
 *             
 * *****************************************************************************/

public class AppMainUpdate : MonoBehaviour
{
    private float time;

    void Start()
    {
        ManagerAssistant.Instance.Initialize();
        UIManager.Instance.Load();
        ConfigManager.Instance.Load(ConfigLoadCompleted);
        DataCenterManager.Instance.Initialize();
    }

    private void ConfigLoadCompleted()
    {
        Server.Instance.Start();
    }

    void Update()
    {
        Server.Instance.Update();
        DataCacheManager.Instance.serverTime += (long)(Time.deltaTime * 1000);
        time += Time.deltaTime;
        if (time > 10)
        {
            time = 0;
            DataCenterManager.Instance.Ping();
        }
    }

    /// <summary>
    /// APP暂停时相应（移动平台退出到后台）
    /// </summary>
    /// <param name="paused"></param>
    void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            Debug.Log("[App Pause]");
            Server.Instance.Pause();
        }
        else
        {
            Debug.Log("[App Resume]");
            Server.Instance.Resume();
        }
    }

    /// <summary>
    /// APP结束时调用
    /// </summary>
    void OnApplicationQuit()
    {
        Debug.Log("[App End]");
        Server.Instance.Stop();
    }
}
