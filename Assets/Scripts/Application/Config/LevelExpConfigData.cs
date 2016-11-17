using System.Collections;

public class LevelExpConfigData
{
	/// <summary>
	/// 等级
	/// <summary>
	public int level { get; private set; }
	/// <summary>
	/// 升级经验
	/// <summary>
	public int exp { get; private set; }

    public LevelExpConfigData(string text)
    {
        string[] arr = text.Split('\t');
        int index = 0;
		this.level = int.Parse(arr[index++]);
		this.exp = int.Parse(arr[index++]);

    }
}