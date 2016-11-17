using System.Collections;

public class AdventureConfigData
{
	/// <summary>
	/// ID
	/// <summary>
	public int id { get; private set; }
	/// <summary>
	/// 说明
	/// <summary>
	public string desc { get; private set; }
	/// <summary>
	/// 奖励
	/// <summary>
	public string rewards { get; private set; }

    public AdventureConfigData(string text)
    {
        string[] arr = text.Split('\t');
        int index = 0;
		this.id = int.Parse(arr[index++]);
		this.desc = arr[index++];
		this.rewards = arr[index++];

    }
}