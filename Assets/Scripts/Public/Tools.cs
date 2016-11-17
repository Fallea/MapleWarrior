using UnityEngine;
using System.Collections;

/*******************************************************************************
 * 
 *             类名: Tools
 *             功能: 工具函数
 *             作者: HGQ
 *             日期: 2016.4.24
 *             修改:
 *             
 * *****************************************************************************/

public class Tools
{

    /// <summary>
    /// 安全添加组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="rGo"></param>
    /// <returns></returns>
    public static T GetComponentSafe<T>(GameObject obj) where T : Component
    {
        var com = obj.GetComponent<T>();
        if (com != null)
        {
            return com;
        }
        else
        {
            return obj.AddComponent<T>();
        }
    }
}
