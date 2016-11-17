using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*******************************************************************************
 * 
 *             类名: UIUtils
 *             功能: UI辅助功能类
 *             作者: HGQ
 *             日期: 2016.4.22
 *             修改:
 *             
 * *****************************************************************************/

public class UIUtils
{

    /// 设置GameObject的localPosition的Y值
    public static void SetY(GameObject go, float y)
    {
        Vector3 temp = go.transform.localPosition;
        temp.y = y;
        go.transform.localPosition = temp;
    }

    /// 设置GameObject的localPosition的X值
    public static void SetX(GameObject go, float x)
    {
        Vector3 temp = go.transform.localPosition;
        temp.x = x;
        go.transform.localPosition = temp;
    }

    /// 设置GameObject的localPosition的X,Y值
    public static void SetXY(GameObject go, float x, float y)
    {
        Vector3 temp = go.transform.localPosition;
        temp.x = x;
        temp.y = y;
        go.transform.localPosition = temp;
    }

    /// 复制GameObject，固定设置了localScale，父对象
    public static GameObject Duplicate(GameObject go, GameObject parent)
    {
        return Duplicate(go, parent.transform);
    }

    /// 复制GameObject，固定设置了localScale，父对象
    public static GameObject Duplicate(GameObject go, Transform parent)
    {
        GameObject result = GameObject.Instantiate(go) as GameObject;
        result.SetActive(true);
        result.transform.SetParent(parent);
        result.transform.localScale = Vector3.one;
        return result;
    }

    //================================================================

}