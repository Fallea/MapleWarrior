using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*******************************************************************************
 * 
 *             类名: AssetManager
 *             功能: 原始资源（非实例对象，即AssetBundle包中加载出来的）管理
 *             作者: HGQ
 *             日期: 2016.06.23
 *             修改: 
 *             
 * *****************************************************************************/

public class AssetManager : TSingleton<AssetManager>
{
    AssetManager() { }

    Dictionary<string, UnityEngine.Object> dicObjects = new Dictionary<string, UnityEngine.Object>();

    public void Add(string key, UnityEngine.Object obj)
    {
        if (dicObjects.ContainsKey(key))
        {
            if (dicObjects[key] != obj)
            {
                Debug.LogWarning(">> AssetManager > Add > " + key + " is existed,please check!!!");
                dicObjects[key] = obj;
            }
        }
        else
        {
            dicObjects.Add(key, obj);
        }
    }

    public UnityEngine.Object Get(string key)
    {
        if (dicObjects.ContainsKey(key))
        {
            return dicObjects[key];
        }
        return null;
    }

    public void Clear()
    {
        dicObjects.Clear();
    }
}
