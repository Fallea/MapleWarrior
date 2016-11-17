using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 区域地图
/// </summary>
public class Area  {
	/// <summary>
	/// 区域ID
	/// </summary>
	public int id;

	/// <summary>
	/// 地图列表
	/// </summary>
	private List<Map> mMaps = new List<Map>();

	public Area Cache(SvDataArea svDataArea)
	{
		this.id = svDataArea.id;
		mMaps.Clear ();
		for (int i = 0, length = svDataArea.maps.Count; i < length; i++) 
		{
			mMaps.Add(new Map().Cache(svDataArea.maps[i]));
		}
		return this;
	}

	/// <summary>
	/// 地图列表
	/// </summary>
	public List<Map> maps
	{
		get{return mMaps;}
	}

	//================================================================

	public static bool IsOpen(AreaConfigData areaConfigData)
	{
		if (areaConfigData == null || string.IsNullOrEmpty (areaConfigData.open)) 
		{
			return true;
		} 
		else 
		{
			string[] mapArr = areaConfigData.open.Trim().Split(',');
			bool isOpen = true;
			for(int i = 0; i < mapArr.Length; i++)
			{
				if(string.IsNullOrEmpty(mapArr[i]))
				{
					continue;
				}
				int mapId = int.Parse(mapArr[i]);
				if(DataCacheManager.Instance.GetMap(mapId) == null)
				{
					return false;
				}
			}
			return true;
		}
	}

}
