using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*******************************************************************************
 * 
 *             类名: ResourcesManager
 *             功能: Resources资源管理
 *             作者: HGQ
 *             日期: 2016.06.23
 *             修改: 
 *             
 * *****************************************************************************/

public class ResourcesManager : TSingleton<ResourcesManager>
{
    ResourcesManager() { }

    public AssetLoader LoadAssetAsync(string assetName, System.Type type)
    {
        AssetLoader loader = new ResourcesAssetLoader(assetName, type);
        return loader;
    }

}
