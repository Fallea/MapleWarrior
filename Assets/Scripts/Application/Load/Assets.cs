using UnityEngine;

public class Assets
{
    //注：所有目录的使用最后都会带“/”，使用时不需在前面添加

    public const string SUFFIX = ".assetbundle";

    /// <summary>
    /// 资源包目录，指StreamingAssets的路径
    /// </summary>
    public static string AssetsPath
    {
        get
        {
            string path = string.Empty;

            if (Application.platform == RuntimePlatform.Android)
            {
                path = "jar:file://" + Application.dataPath + "!/assets/";
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                path = Application.dataPath + "/Raw/";
            }
            else
            {
                path = Application.dataPath + "/StreamingAssets/";
            }
            return path;
        }
    }

    /// <summary>
    /// 运行时资源路径，资源拷贝和下载存放的路径，也是运行时资源读取的路径
    /// </summary>
    public static string RuntimeAssetsPath
    {
        get
        {
            if (Application.isMobilePlatform)
            {
                return Application.persistentDataPath + "/";
            }
            return Application.dataPath + "/StreamingAssets/";
        }
    }
    /// <summary>
    /// 获得运行时资源包的路径（本地资源），AssetBundle读取路径，即路径前面添加了file://
    /// </summary>
    /// <returns></returns>
    public static string AssetBundleUrlPath
    {
        get
        {
            return "file://" + RuntimeAssetsPath;
        }
    }


}