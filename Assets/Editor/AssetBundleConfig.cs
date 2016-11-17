using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

[System.Serializable]
public class AssetBundleInfo
{
    /// <summary>
    /// 资源包名，如果是Dir_All类型的打包方式则为文件夹
    /// </summary>
    public string bundleName;

    /// <summary>
    /// 资源包的原始路径
    /// </summary>
    public string assetPath;

    /// <summary>
    /// 资源包的variant名
    /// </summary>
    public string variantName;

    /// <summary>
    /// 打包方式Dir_All(整个文件夹下的文件打一个包，不包含再下级的文件夹)、Dir_File(文件夹下的文件单个打包)、File(一个文件打包)
    /// </summary>
    public string packageType;

    /// <summary>
    /// 获取打包的列表
    /// </summary>
    /// <returns></returns>
    public AssetBundleBuild[] GetBuildList()
    {
        if (string.IsNullOrEmpty(bundleName) || string.IsNullOrEmpty(assetPath))
        {
            Debug.LogWarning(">> AssetBundleInfo > " + bundleName + " , " + assetPath);
            return null;
        }

        if ("Dir_All".Equals(packageType))
        {
            return GetOrMarkDirAll();
        }
        else if ("Dir_File".Equals(packageType))
        {
            return GetOrMarkDirFile();
        }
        else if ("File".Equals(packageType))
        {
            return GetOrMarkFile();
        }
        else
        {
            Debug.LogWarning(">> PackageType is not exist. > " + packageType);
        }
        return null;
    }

    /// <summary>
    /// 标记BundleName，使用直接读取本地引用的Bundle
    /// </summary>
    public void MarkBundleName()
    {
        if (string.IsNullOrEmpty(bundleName) || string.IsNullOrEmpty(assetPath))
        {
            Debug.LogWarning(">> AssetBundleInfo > " + bundleName + " , " + assetPath);
            return;
        }

        if ("Dir_All".Equals(packageType))
        {
            GetOrMarkDirAll(true);
        }
        else if ("Dir_File".Equals(packageType))
        {
            GetOrMarkDirFile(true);
        }
        else if ("File".Equals(packageType))
        {
            GetOrMarkFile(true);
        }
        else
        {
            Debug.LogWarning(">> PackageType is not exist. > " + packageType);
        }
    }

    private AssetBundleBuild[] GetOrMarkDirAll(bool isMark = false)
    {
        DirectoryInfo dirInfo = new DirectoryInfo(this.assetPath);
        if (!dirInfo.Exists)
        {
            return null;
        }

        List<string> list = new List<string>();

        FileInfo[] fileInfos = dirInfo.GetFiles();
        foreach (FileInfo fileInfo in fileInfos)
        {
            if (".meta".Equals(fileInfo.Extension))
            {
                continue;
            }
            string tempPath = fileInfo.FullName.Replace('\\', '/');
            list.Add(tempPath);
        }

        string dataPath = Application.dataPath;
        dataPath = dataPath.Replace('\\', '/');

        if (isMark)
        {
            foreach (string file in list)
            {
                string temp = file.Replace(dataPath, "Assets");
                AssetImporter importer = AssetImporter.GetAtPath(temp);
                if (importer != null)
                {
                    importer.assetBundleName = bundleName + Assets.SUFFIX;
                    importer.assetBundleVariant = variantName;
                }
                else
                {
                    Debug.LogWarning(">> AssetImporter > " + temp);
                }
            }
            return null;
        }
        else
        {
            AssetBundleBuild abb = new AssetBundleBuild();
            abb.assetBundleName = bundleName + Assets.SUFFIX;
            abb.assetBundleVariant = variantName;
            abb.assetNames = list.ToArray();

            AssetBundleBuild[] builds = new AssetBundleBuild[1];
            builds[0] = abb;

            return builds;
        }
    }

    private AssetBundleBuild[] GetOrMarkDirFile(bool isMark = false)
    {
        DirectoryInfo dirInfo = new DirectoryInfo(this.assetPath);
        if (!dirInfo.Exists)
        {
            return null;
        }

        List<string> list = new List<string>();

        FileInfo[] fileInfos = dirInfo.GetFiles();
        foreach (FileInfo fileInfo in fileInfos)
        {
            if (".meta".Equals(fileInfo.Extension))
            {
                continue;
            }
            string tempPath = fileInfo.FullName.Replace('\\', '/');
            list.Add(tempPath);
        }

        string dataPath = Application.dataPath;
        dataPath = dataPath.Replace('\\', '/');

        if (isMark)
        {
            foreach (string file in list)
            {
                string temp = file.Replace(dataPath, "Assets");
                AssetImporter importer = AssetImporter.GetAtPath(temp);
                if (importer != null)
                {
                    importer.assetBundleName = bundleName + "/" + Path.GetFileNameWithoutExtension(temp) + Assets.SUFFIX;
                    importer.assetBundleVariant = variantName;
                }
                else
                {
                    Debug.LogWarning(">> AssetImporter > " + temp);
                }
            }
            return null;
        }
        else
        {
            AssetBundleBuild[] builds = new AssetBundleBuild[list.Count];
            for (int i = 0; i < builds.Length; i++)
            {
                string temp = list[i].Replace(dataPath, "Assets");
                AssetBundleBuild abb = new AssetBundleBuild();
                abb.assetBundleName = bundleName + "/" + Path.GetFileNameWithoutExtension(temp) + Assets.SUFFIX;
                abb.assetBundleVariant = variantName;
                abb.assetNames = new string[] { temp };
                builds[i] = abb;
            }
            return builds;
        }
    }

    private AssetBundleBuild[] GetOrMarkFile(bool isMark = false)
    {
        if (!File.Exists(this.assetPath))
        {
            return null;
        }

        string dataPath = Application.dataPath;
        dataPath = dataPath.Replace('\\', '/');

        if (isMark)
        {
            string temp = this.assetPath;
            AssetImporter importer = AssetImporter.GetAtPath(temp);
            if (importer != null)
            {
                importer.assetBundleName = bundleName + Assets.SUFFIX;
                importer.assetBundleVariant = variantName;
            }
            else
            {
                Debug.LogWarning(">> AssetImporter > " + temp);
            }
            return null;
        }
        else
        {
            AssetBundleBuild[] builds = new AssetBundleBuild[1];
            string temp = this.assetPath;
            AssetBundleBuild abb = new AssetBundleBuild();
            abb.assetBundleName = bundleName + Assets.SUFFIX;
            abb.assetBundleVariant = variantName;
            abb.assetNames = new string[] { temp };
            builds[0] = abb;
            return builds;
        }
    }
}

[System.Serializable]
public class AssetBundleConfig
{
    public AssetBundleInfo[] bundles;
}
