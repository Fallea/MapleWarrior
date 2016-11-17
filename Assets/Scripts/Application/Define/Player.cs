
public class Player
{
	/// <summary>
	/// 玩家ID
	/// </summary>
	public int id;
	/// <summary>
	/// 玩家名称
	/// </summary>
	public string name;
	/// <summary>
	/// 性别
	/// </summary>
	public int sex = 0;
    /// <summary>
    /// 经验
    /// </summary>
    public int exp;
	/// <summary>
	/// 等级
	/// </summary>
	public int level = 1;
	/// <summary>
	/// 现金，钻石
	/// </summary>
	public int money;
	/// <summary>
	/// 金币
	/// </summary>
	public int gold;
	/// <summary>
	/// 行动力
	/// </summary>
	public int power = 0;
	/// <summary>
	/// 行动力上限
	/// </summary>
	public int powerMax = 1;
	/// <summary>
	/// 上次更新体力时间
	/// </summary>
	public long lastPowerTime;

	public void Cache(SvDataPlayer svDataPlayer)
	{
		this.id = svDataPlayer.id;
		this.name = svDataPlayer.name;
		this.sex = svDataPlayer.sex;
		this.level = svDataPlayer.level;
		this.money = svDataPlayer.money;
		this.gold = svDataPlayer.gold;
		this.power = svDataPlayer.power;
		this.powerMax = svDataPlayer.powerMax;
		this.lastPowerTime = svDataPlayer.lastPowerTime;
	}
}
