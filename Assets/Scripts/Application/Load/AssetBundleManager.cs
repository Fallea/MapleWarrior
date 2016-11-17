using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR	
using UnityEditor;
#endif

/*******************************************************************************
 * 
 *             类名: AssetManager
 *             功能: AssetBundle包资源管理
 *             作者: HGQ
 *             日期: 2016.06.23
 *             修改: 
 *             
 * *****************************************************************************/

public class AssetBundleManager : TSingleton<AssetBundleManager>
{
    AssetBundleManager() { }

    private string bundlePath = "";
    private Dictionary<string, LoadedAssetBundle> loadedBundles = new Dictionary<string, LoadedAssetBundle>();

    /// <summary>
    /// 获取一个LoadedAssetBundle对象，主要是处理WWW加载相关
    /// </summary>
    /// <param name="assetBundleName"></param>
    /// <returns></returns>
    public LoadedAssetBundle GetLoadedAssetBundle(string assetBundleName)
    {
        if (loadedBundles.ContainsKey(assetBundleName))
        {
            loadedBundles[assetBundleName].referencedCount++;
            Debug.Log(">> AssetBundleManager > " + assetBundleName + " > referencedCount > " + loadedBundles[assetBundleName].referencedCount);
            return loadedBundles[assetBundleName];
        }
        LoadedAssetBundle lab = new LoadedAssetBundle(bundlePath + assetBundleName);
        loadedBundles.Add(assetBundleName, lab);
        return lab;
    }


    public AssetLoader LoadAssetAsync(string assetBundleName, string assetName, System.Type type)
    {
        AssetLoader loader = null;
#if UNITY_EDITOR
        if (SimulateAssetBundleInEditor)
        {
            assetBundleName = assetBundleName.ToLower();
            string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(assetBundleName, assetName);
            if (assetPaths.Length == 0)
            {
                Debug.LogError("There is no asset with name \"" + assetName + "\" in " + assetBundleName);
                return null;
            }
            // @TODO: Now we only get the main object from the first asset. Should consider type also.
            Object obj = AssetDatabase.LoadMainAssetAtPath(assetPaths[0]);
            loader = new AssetBundleSimulationLoader(obj);
        }
        else
#endif
        {
            //LoadAssetBundle(assetBundleName);
            loader = new AssetBundleAssetLoader(assetBundleName, assetName, type);

            //m_InProgressOperations.Add(operation);
        }

        return loader;
    }

    //================================================================

    private static int mSimulateAssetBundleInEditor = -1;
    private const string SIMULATE_ASSETBUNDLE = "SimulateAssetBundle";

    public static bool SimulateAssetBundleInEditor
    {
        get
        {

#if UNITY_EDITOR
            if (mSimulateAssetBundleInEditor == -1)
            {
                mSimulateAssetBundleInEditor = EditorPrefs.GetBool(SIMULATE_ASSETBUNDLE, true) ? 1 : 0;
            }
#endif
            return mSimulateAssetBundleInEditor != 0;
        }
        set
        {
            int newValue = value ? 1 : 0;
            if (newValue != mSimulateAssetBundleInEditor)
            {
                mSimulateAssetBundleInEditor = newValue;
#if UNITY_EDITOR
                EditorPrefs.SetBool(SIMULATE_ASSETBUNDLE, value);
#endif
            }
        }
    }

}
