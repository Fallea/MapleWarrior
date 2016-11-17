using System.Collections;

public class BoxConfigData
{
	/// <summary>
	/// ID
	/// <summary>
	public int id { get; private set; }
	/// <summary>
	/// 介绍
	/// <summary>
	public string desc { get; private set; }
	/// <summary>
	/// 奖励
	/// <summary>
	public string rewards { get; private set; }

    public BoxConfigData(string text)
    {
        string[] arr = text.Split('\t');
        int index = 0;
		this.id = int.Parse(arr[index++]);
		this.desc = arr[index++];
		this.rewards = arr[index++];

    }
}