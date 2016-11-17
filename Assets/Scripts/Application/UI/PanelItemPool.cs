using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelItemPool<T> where T : UIViewItem{

	private List<T> list = new List<T>();

	private GameObject prefab;

	public void SetPrefab(GameObject go)
	{
		prefab = go;
	}

	public T Get()
	{
		T result;
		if (list.Count > 0) {
			result = list [list.Count - 1];
			list.RemoveAt (list.Count - 1);
		} else {
			GameObject newGo = GameObject.Instantiate(prefab) as GameObject;
			newGo.transform.SetParent(prefab.transform.parent);
			newGo.transform.localPosition = Vector3.zero;
			newGo.transform.localScale = Vector3.one;
			result = newGo.GetComponent<T>();
		}
		result.gameObject.SetActive (true);
        result.transform.SetAsLastSibling();

        return result;
	}

	public void Recovery(T item)
	{
		list.Add (item);
		item.gameObject.SetActive (false);
	}
}
