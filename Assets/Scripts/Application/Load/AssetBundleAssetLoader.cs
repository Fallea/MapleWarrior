using UnityEngine;
using System.Collections;

/*******************************************************************************
 * 
 *             类名: AssetBundleAssetLoader
 *             功能: AssetBundle资源加载
 *             作者: HGQ
 *             日期: 2016.06.23
 *             修改: 
 *             
 * *****************************************************************************/

public class AssetBundleAssetLoader : AssetLoader
{
    protected string mAssetBundleName;
    protected string mAssetName;
    protected string mLoadError;
    protected System.Type mType;
    protected LoadedAssetBundle mLoaded = null;
    protected AssetBundleRequest mRequest = null;

    public AssetBundleAssetLoader(string assetBundleName, string assetName, System.Type type)
    {
        mAssetBundleName = assetBundleName;
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
        if (mLoaded == null)
        {
            mLoaded = AssetBundleManager.Instance.GetLoadedAssetBundle(mAssetBundleName);
            Debug.Log(">> Load > HashCode > " + mLoaded.GetHashCode());
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
            if (mLoaded != null && mLoaded.IsDone())
            {
                if (string.IsNullOrEmpty(mLoaded.error))
                {
                    mRequest = mLoaded.assetBundle.LoadAssetAsync(mAssetName, mType);
                }
                else
                {
                    mLoadError = mLoaded.error;
                    Debug.LogError(mLoadError);
                    return true;
                }
            }
        }

        return mRequest != null && mRequest.isDone;
    }

    public override void Clear()
    {
        mAssetBundleName = null;
        mAssetName = null;
        mLoadError = null;
        mLoaded = null;
        mRequest = null;
    }

}

