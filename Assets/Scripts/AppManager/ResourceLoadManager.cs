using UnityEngine;
using System;
using System.Collections;

/*******************************************************************************
 * 
 *             类名: ResourceLoadManager
 *             功能: 资源加载管理
 *             作者: HGQ
 *             日期: 2016.4.22
 *             修改:
 *             
 * *****************************************************************************/

public class ResourceLoadManager : TManager<ResourceLoadManager>
{

    void Start()
    {
        //this.enabled = false;
    }


    void Update()
    {

    }

    //================================================================
    /// <summary>
    /// 异步加载资源(非实例化资源)，根据资源包加载资源并回调资源对象，使用Resources资源方式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetName">Resources下的完整路径</param>
    /// <param name="callback"></param>
    public void LoadAssetAsync<T>(string assetName, Action<T> callback) where T : UnityEngine.Object
    {
        string key = assetName;

        UnityEngine.Object obj = AssetManager.Instance.Get(key);
        if (obj != null)
        {
            if (callback != null)
            {
                callback.Invoke(obj as T);
            }
        }
        else
        {
            StartCoroutine(OnLoadAsset<T>(assetName, callback));
        }
    }

    IEnumerator OnLoadAsset<T>(string assetName, Action<T> callback) where T : UnityEngine.Object
    {
        AssetLoader loader = ResourcesManager.Instance.LoadAssetAsync(assetName, typeof(T));
        if (loader == null)
        {
            yield break;
        }

        yield return StartCoroutine(loader);

        T asset = loader.GetAsset<T>();
        loader.Clear();
        if (asset != null)
        {
            AssetManager.Instance.Add(assetName, asset);
            if (callback != null)
            {
                callback.Invoke(asset);
            }
        }
        else
        {
            Debug.LogError("Asset load failed : Asset[" + assetName + "], Type[" + typeof(T) + "]");
        }

        yield return null;
    }


    //================================================================


    /// <summary>
    /// 异步加载资源(非实例化资源)，根据资源包加载资源并回调资源对象，使用AssetBundle包方式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetBundleName">资源的完整路径，AssetBundle包路径为相对资源目录的跟路径，引用资源为设置的路径</param>
    /// <param name="assetName"></param>
    /// <param name="callback"></param>
    public void LoadAssetAsync<T>(string assetBundleName, string assetName, Action<T> callback) where T : UnityEngine.Object
    {
        string key = assetBundleName + "_" + assetName;//该key是组合key，与resources下的加载key完全没有冲突，所有可以共用AssetManager来管理

        UnityEngine.Object obj = AssetManager.Instance.Get(key);
        if (obj != null)
        {
            if (callback != null)
            {
                Debug.Log("Check > " + key);
                callback.Invoke(obj as T);
            }
        }
        else
        {
            StartCoroutine(OnLoadAsset<T>(assetBundleName, assetName, callback));
        }
    }

    IEnumerator OnLoadAsset<T>(string assetBundleName, string assetName, Action<T> callback) where T : UnityEngine.Object
    {
        AssetLoader loader = AssetBundleManager.Instance.LoadAssetAsync(assetBundleName, assetName, typeof(T));
        if (loader == null)
        {
            yield break;
        }

        yield return StartCoroutine(loader);

        T asset = loader.GetAsset<T>();
        loader.Clear();
        if (asset != null)
        {
            AssetManager.Instance.Add(assetBundleName + "_" + assetName, asset);
            if (callback != null)
            {
                callback.Invoke(asset);
            }
        }
        else
        {
            Debug.LogError("Asset load failed : Bundle[" + assetBundleName + "]" + ", Asset[" + assetName + "], Type[" + typeof(T) + "]");
        }

        // 释放assetBundle
        //AssetBundleManager.UnloadAssetBundle(assetBundleName);
        yield return null;
    }

}
