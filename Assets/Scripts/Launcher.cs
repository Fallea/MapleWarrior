using UnityEngine;
using System;
using System.Collections;

/*******************************************************************************
 * 
 *             类名: Launcher
 *             功能: 启动入口
 *             作者: HGQ
 *             日期: 2016.4.22
 *             修改: 
 *             
 * *****************************************************************************/

public class Launcher : MonoBehaviour
{
    public UIViewRoot uiRoot;

    void Awake()
    {
        UIManager.Instance.uiRoot = uiRoot;
        UIManager.Instance.Initialize();

        InitAppMainUpdate();
    }

    /// <summary>
    /// 初始化App，并启动App
    /// </summary>
    void InitAppMainUpdate()
    {
        string strName = "AppMainUpdate";
        GameObject main = GameObject.Find(strName);
        if (main == null)
        {
            main = new GameObject(strName);
            GameObject.DontDestroyOnLoad(main);
        }
        Tools.GetComponentSafe<AppMainUpdate>(main);
    }

}
