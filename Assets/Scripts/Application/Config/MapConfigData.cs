using System.Collections;

public class MapConfigData
{
	/// <summary>
	/// ID
	/// <summary>
	public int id { get; private set; }
	/// <summary>
	/// 名称
	/// <summary>
	public string name { get; private set; }
	/// <summary>
	/// 备注
	/// <summary>
	public string remark { get; private set; }
	/// <summary>
	/// 槽位
	/// <summary>
	public int slot { get; private set; }
	/// <summary>
	/// 地图类型
	/// <summary>
	public MapType type { get; private set; }
	/// <summary>
	/// 区域ID
	/// <summary>
	public int areaId { get; private set; }
	/// <summary>
	/// 上一个地图
	/// <summary>
	public int lastId { get; private set; }
	/// <summary>
	/// 行动力
	/// <summary>
	public int power { get; private set; }
	/// <summary>
	/// 时间
	/// <summary>
	public float time { get; private set; }

    public MapConfigData(string text)
    {
        string[] arr = text.Split('\t');
        int index = 0;
		this.id = int.Parse(arr[index++]);
		this.name = arr[index++];
		this.remark = arr[index++];
		this.slot = int.Parse(arr[index++]);
		this.type = (MapType)int.Parse(arr[index++]);
		this.areaId = int.Parse(arr[index++]);
		this.lastId = int.Parse(arr[index++]);
		this.power = int.Parse(arr[index++]);
		this.time = float.Parse(arr[index++]);

    }
}