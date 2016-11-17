using System.Collections.Generic;

/// <summary>
/// 存储数据
/// </summary>
public class SvDataPlayer
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
    public int power = 60;
    /// <summary>
    /// 行动力上限
    /// </summary>
    public int powerMax = 60;

	/// <summary>
	/// 上一次计算体力时间
	/// </summary>
	public long lastPowerTime;

    /// <summary>
    /// 卡牌列表
    /// </summary>
    public List<SvDataCard> cards = new List<SvDataCard>();
	/// <summary>
	/// 地图区域列表
	/// </summary>
	public List<SvDataArea> areas = new List<SvDataArea>();

	public SvDataPlayer()
	{
		lastPowerTime = Util.GetTime ();
	}
}
