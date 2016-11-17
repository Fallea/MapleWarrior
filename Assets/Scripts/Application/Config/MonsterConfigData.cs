using System.Collections;

public class MonsterConfigData
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
	/// 类型
	/// <summary>
	public CardType type { get; private set; }
	/// <summary>
	/// 等级
	/// <summary>
	public int level { get; private set; }
	/// <summary>
	/// 图标
	/// <summary>
	public string icon { get; private set; }
	/// <summary>
	/// 品质
	/// <summary>
	public int quality { get; private set; }
	/// <summary>
	/// 星级
	/// <summary>
	public int star { get; private set; }
	/// <summary>
	/// 体力
	/// <summary>
	public float hp { get; private set; }
	/// <summary>
	/// 物理攻击力
	/// <summary>
	public float physicalAttack { get; private set; }
	/// <summary>
	/// 物理防御
	/// <summary>
	public float physicalDefense { get; private set; }
	/// <summary>
	/// 魔法攻击力
	/// <summary>
	public float magicAttack { get; private set; }
	/// <summary>
	/// 魔法防御
	/// <summary>
	public float magicDefense { get; private set; }
	/// <summary>
	/// 速度
	/// <summary>
	public float speed { get; private set; }
	/// <summary>
	/// 暴击率
	/// <summary>
	public int critChance { get; private set; }
	/// <summary>
	/// 暴击伤害
	/// <summary>
	public int critDamage { get; private set; }

    public MonsterConfigData(string text)
    {
        string[] arr = text.Split('\t');
        int index = 0;
		this.id = int.Parse(arr[index++]);
		this.name = arr[index++];
		this.type = (CardType)int.Parse(arr[index++]);
		this.level = int.Parse(arr[index++]);
		this.icon = arr[index++];
		this.quality = int.Parse(arr[index++]);
		this.star = int.Parse(arr[index++]);
		this.hp = float.Parse(arr[index++]);
		this.physicalAttack = float.Parse(arr[index++]);
		this.physicalDefense = float.Parse(arr[index++]);
		this.magicAttack = float.Parse(arr[index++]);
		this.magicDefense = float.Parse(arr[index++]);
		this.speed = float.Parse(arr[index++]);
		this.critChance = int.Parse(arr[index++]);
		this.critDamage = int.Parse(arr[index++]);

    }
}