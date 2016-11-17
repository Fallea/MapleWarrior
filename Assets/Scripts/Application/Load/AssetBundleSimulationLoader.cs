using UnityEngine;

public class AssetBundleSimulationLoader : AssetLoader
{
    Object mSimulatedObject;

    public AssetBundleSimulationLoader(Object obj)
    {
        mSimulatedObject = obj;
    }

    public override T GetAsset<T>()
    {
        return mSimulatedObject as T;
    }

    public override void Load()
    {
    }

    public override bool Update()
    {
        return false;
    }

    public override bool IsDone()
    {
        return true;
    }

    public override void Clear()
    {
        mSimulatedObject = null;
    }
}
