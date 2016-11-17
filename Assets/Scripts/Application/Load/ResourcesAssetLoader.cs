using UnityEngine;
using System.Collections;
using System;

/*******************************************************************************
 * 
 *             类名: ResourcesAssetLoader
 *             功能: Resources资源加载器
 *             作者: HGQ
 *             日期: 2016.06.23
 *             修改: 
 *             
 * *****************************************************************************/

public class ResourcesAssetLoader : AssetLoader
{
    protected string mAssetName;
    protected string mLoadError;
    protected System.Type mType;
    protected ResourceRequest mRequest = null;

    public ResourcesAssetLoader(string assetName, System.Type type)
    {
        mAssetName = assetName;
        mType = type;
        Load();
    }

    public override T GetAsset<T>()
    {
        if (mRequest != null && mRequest.isDone)
        {
            return mRequest.asset as T;
        }
        return null;
    }

    public override void Load()
    {
        if (mRequest == null)
        {
            mRequest = Resources.LoadAsync(mAssetName, mType);
        }
    }

    public override bool Update()
    {
        return false;
    }

    public override bool IsDone()
    {
        if (mRequest == null)
        {
            return false;
        }
        else
        {
            return mRequest.isDone;
        }
    }

    public override void Clear()
    {
        mAssetName = null;
        mRequest = null;
    }

}


