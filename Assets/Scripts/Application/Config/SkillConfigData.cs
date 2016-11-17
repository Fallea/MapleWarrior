using System.Collections;

public class SkillConfigData
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
	/// 图标
	/// <summary>
	public string icon { get; private set; }
	/// <summary>
	/// 介绍
	/// <summary>
	public string desc { get; private set; }

    public SkillConfigData(string text)
    {
        string[] arr = text.Split('\t');
        int index = 0;
		this.id = int.Parse(arr[index++]);
		this.name = arr[index++];
		this.icon = arr[index++];
		this.desc = arr[index++];

    }
}