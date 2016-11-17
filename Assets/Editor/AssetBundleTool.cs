using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

/*******************************************************************************
 * 
 *             类名: AssetBundleTool
 *             功能: 用于资源打包
 *             作者: HGQ
 *             日期: 2016.4.22
 *             修改:
 *             
 * *****************************************************************************/

public class AssetBundleTool
{
    private const string SIMULATE_MODE = "Tools/AssetBundle/Simulation Mode";

    private const string AssetBundleConfigPath = "Editor/AssetBundleConfig.txt";

    /// <summary>
    /// 切换是读取bundle还是原始资源
    /// </summary>
    [MenuItem(SIMULATE_MODE)]
    public static void ToggleSimulationMode()
    {
        AssetBundleManager.SimulateAssetBundleInEditor = !AssetBundleManager.SimulateAssetBundleInEditor;
    }

    [MenuItem(SIMULATE_MODE, true)]
    public static bool ToggleSimulationModeValidate()
    {
        Menu.SetChecked(SIMULATE_MODE, AssetBundleManager.SimulateAssetBundleInEditor);
        return true;
    }

    /// <summary>
    /// 清除之前设置过的AssetBundleName，避免产生不必要的资源也打包
    /// 只要设置了AssetBundleName的，都会进行打包，不论在什么目录下
    /// </summary>
    public static void ClearAssetBundlesName()
    {
        string[] bundleNames = AssetDatabase.GetAllAssetBundleNames();
        int length = bundleNames.Length;
        string[] oldAssetBundleNames = new string[length];
        for (int i = 0; i < length; i++)
        {
            oldAssetBundleNames[i] = bundleNames[i];
        }

        for (int j = 0; j < oldAssetBundleNames.Length; j++)
        {
            AssetDatabase.RemoveAssetBundleName(oldAssetBundleNames[j], true);
        }
        length = AssetDatabase.GetAllAssetBundleNames().Length;
        Debug.Log(">> Clear AssetBundle Name Compelte. > " + length);
    }

    [MenuItem("Tools/AssetBundle/Mark AssetBundle Config")]
    private static void MarkAssetBundleConfig()
    {
        string path = Application.dataPath + "/" + AssetBundleConfigPath;
        if (!File.Exists(path))
        {
            return;
        }

        ClearAssetBundlesName();

        string str = File.ReadAllText(path);
        AssetBundleConfig config = JsonHelper.Deserialize<AssetBundleConfig>(str);

        if (config == null || config.bundles == null)
        {
            return;
        }

        for (int i = 0; i < config.bundles.Length; i++)
        {
            //BuildPipeline.BuildAssetBundles(config.bundles, list.ToArray(), BuildAssetBundleOptions.None | BuildAssetBundleOptions.ForceRebuildAssetBundle, EditorUserBuildSettings.activeBuildTarget);
            config.bundles[i].MarkBundleName();
        }
        Debug.Log(">> Mark AssetBundle Config Compelte.");
    }

    [MenuItem("Tools/AssetBundle/Build AssetBundle Config")]
    private static void BuildAssetBundleConfig()
    {
        string path = Application.dataPath + "/" + AssetBundleConfigPath;
        if (!File.Exists(path))
        {
            return;
        }

        ClearAssetBundlesName();

        string str = File.ReadAllText(path);
        AssetBundleConfig config = JsonHelper.Deserialize<AssetBundleConfig>(str);

        if (config == null || config.bundles == null)
        {
            return;
        }

        List<AssetBundleBuild> list = new List<AssetBundleBuild>();

        for (int i = 0; i < config.bundles.Length; i++)
        {
            list.AddRange(config.bundles[i].GetBuildList());
        }
        string bundlesPath = Application.dataPath + "/StreamingAssets";
        BuildPipeline.BuildAssetBundles(bundlesPath, list.ToArray(), BuildAssetBundleOptions.None | BuildAssetBundleOptions.ForceRebuildAssetBundle, EditorUserBuildSettings.activeBuildTarget);
        Debug.Log(">> Build AssetBundle Config Compelte.");
        AssetDatabase.Refresh();
    }

    //================================================================


    [MenuItem("Tools/AssetBundle/Build Selected Item")]
    static void ToolsBuildSelectItem()
    {
        BuildSelectItem();
    }

    [MenuItem("Assets/AssetBundle/Build Selected Item")]
    static void AssetsBuildSelectItem()
    {
        BuildSelectItem();
    }

    static void BuildSelectItem()
    {
        if (Selection.objects.Length > 1)
        {
            Debug.LogWarning(">> Please select single file.");
            return;
        }

        if (Selection.activeObject == null)
        {
            return;
        }

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        path = path.Replace("\\", "/");
        path = path.Replace("Assets/", "StreamingAssets/");

        string suffix = path.Substring(path.IndexOf("."));
        path = Application.dataPath + "/" + path.Replace(suffix, ".assetbundle");

        string[] arr = path.Split('/');
        string dir = path.Replace(arr[arr.Length - 1], "");

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        BuildPipeline.BuildAssetBundle(Selection.activeObject, null, path, BuildAssetBundleOptions.CollectDependencies, EditorUserBuildSettings.activeBuildTarget);
        AssetDatabase.Refresh();
    }

    //================================================================




}
