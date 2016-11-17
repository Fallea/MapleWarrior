using System.Collections;

/*******************************************************************************
 * 
 *             类名: AssetLoader
 *             功能: 资源加载
 *             作者: HGQ
 *             日期: 2016.06.23
 *             修改: 
 *             
 * *****************************************************************************/

public abstract class AssetLoader : IEnumerator
{
    public object Current
    {
        get
        {
            return null;
        }
    }

    public bool MoveNext()
    {
        return !IsDone();
    }

    public void Reset()
    {
    }

    public abstract T GetAsset<T>() where T : UnityEngine.Object;

    public abstract void Load();

    public abstract bool Update();

    public abstract bool IsDone();

    public abstract void Clear();

}
