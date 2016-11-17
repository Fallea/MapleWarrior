

public class Card  {

	public long id;

    public int cardId;

    public int level = 1;

    public int exp;

    public int star;

    /// <summary>
    /// 力量
    /// </summary>
	public float strength;
    /// <summary>
    /// 敏捷
    /// </summary>
	public float agility;
    /// <summary>
    /// 智力
    /// </summary>
	public float intellect;

    /// <summary>
    /// 体力
    /// </summary>
	public float hp;
    /// <summary>
    /// 物理攻击力
    /// </summary>
	public float physicalAttack;
    /// <summary>
    /// 物理防御
    /// </summary>
	public float physicalDefense;
    /// <summary>
    /// 魔法攻击力
    /// </summary>
	public float magicAttack;
    /// <summary>
    /// 魔法防御
    /// </summary>
	public float magicDefense;
    /// <summary>
    /// 速度
    /// </summary>
	public float speed;
    /// <summary>
    /// 暴击率%
    /// </summary>
	public float critChance;
    /// <summary>
    /// 暴击伤害%
    /// </summary>
	public float critDamage;

	public Card Cache(SvDataCard svDataCard)
	{
		this.id = svDataCard.id;

		this.cardId = svDataCard.cardId;
		this.level = svDataCard.level;
		this.exp = svDataCard.exp;
		this.star = svDataCard.star;

		this.strength = svDataCard.strength;
		this.agility = svDataCard.agility;
		this.intellect = svDataCard.intellect;

		this.hp = svDataCard.hp;
		this.physicalAttack = svDataCard.physicalAttack;
		this.physicalDefense = svDataCard.physicalDefense;
		this.magicAttack = svDataCard.magicAttack;
		this.magicDefense = svDataCard.magicDefense;
		this.speed = svDataCard.speed;
		this.critChance = svDataCard.critChance;
		this.critDamage = svDataCard.critDamage;
		
		return this;
	}

    public void Clear()
    {
    }
}
