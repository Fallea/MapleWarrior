using System.Collections;

public class ErrorConfigData
{
	/// <summary>
	/// 错误ID
	/// <summary>
	public int id { get; private set; }
	/// <summary>
	/// 提示信息
	/// <summary>
	public string msg { get; private set; }

    public ErrorConfigData(string text)
    {
        string[] arr = text.Split('\t');
        int index = 0;
		this.id = int.Parse(arr[index++]);
		this.msg = arr[index++];

    }
}