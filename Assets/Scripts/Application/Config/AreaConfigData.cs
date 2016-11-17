using System.Collections;

public class AreaConfigData
{
	/// <summary>
	/// ID
	/// <summary>
	public int id { get; private set; }
	/// <summary>
	/// 村庄名称
	/// <summary>
	public string name { get; private set; }
	/// <summary>
	/// 槽位
	/// <summary>
	public int slot { get; private set; }
	/// <summary>
	/// 探索等级
	/// <summary>
	public int level { get; private set; }
	/// <summary>
	/// 图标
	/// <summary>
	public string icon { get; private set; }
	/// <summary>
	/// 开启条件
	/// <summary>
	public string open { get; private set; }

    public AreaConfigData(string text)
    {
        string[] arr = text.Split('\t');
        int index = 0;
		this.id = int.Parse(arr[index++]);
		this.name = arr[index++];
		this.slot = int.Parse(arr[index++]);
		this.level = int.Parse(arr[index++]);
		this.icon = arr[index++];
		this.open = arr[index++];

    }
}