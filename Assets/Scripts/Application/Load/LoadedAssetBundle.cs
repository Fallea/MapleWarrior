using UnityEngine;
using System.Collections;

/*******************************************************************************
 * 
 *             类名: LoadedAssetBundle
 *             功能: AssetBundle资源加载
 *             作者: HGQ
 *             日期: 2016.06.24
 *             修改: 
 *             
 * *****************************************************************************/

public class LoadedAssetBundle
{
    public AssetBundle assetBundle = null;
    public int referencedCount = 1;
    public string error = null;

    private bool mIsDone;
    private WWW mLoadWWW = null;

    public LoadedAssetBundle(string assetBundlePath)
    {
        mLoadWWW = new WWW(Assets.AssetBundleUrlPath + assetBundlePath);
    }

    public bool IsDone()
    {
        if (mIsDone)
        {
            return mIsDone;
        }

        if (mLoadWWW != null && mLoadWWW.isDone)
        {
            mIsDone = true;

            if (string.IsNullOrEmpty(mLoadWWW.error))
            {
                assetBundle = mLoadWWW.assetBundle;
            }
            else
            {
                error = string.Format("[LoadedAssetBundle > {0}]", mLoadWWW.error);
            }
            mLoadWWW.Dispose();
            mLoadWWW = null;
            return mIsDone;
        }

        return false;
    }
}