using System.Collections;

public class FightConfigData
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
	/// 怪物列表
	/// <summary>
	public string monsters { get; private set; }
	/// <summary>
	/// 金币
	/// <summary>
	public int gold { get; private set; }
	/// <summary>
	/// 勇士经验
	/// <summary>
	public int exp { get; private set; }
	/// <summary>
	/// 介绍
	/// <summary>
	public string desc { get; private set; }
	/// <summary>
	/// 其他奖励
	/// <summary>
	public string rewards { get; private set; }

    public FightConfigData(string text)
    {
        string[] arr = text.Split('\t');
        int index = 0;
		this.id = int.Parse(arr[index++]);
		this.name = arr[index++];
		this.monsters = arr[index++];
		this.gold = int.Parse(arr[index++]);
		this.exp = int.Parse(arr[index++]);
		this.desc = arr[index++];
		this.rewards = arr[index++];

    }
}