

/// <summary>
/// 奖励
/// </summary>
public class Reward
{
    /// <summary>
    /// 奖励类型
    /// </summary>
    public RewardType type = RewardType.None;
    /// <summary>
    /// 奖励数量
    /// </summary>
    public int num = 0;
    /// <summary>
    /// 奖励的道具ID
    /// </summary>
    public int id;

    public Reward(RewardType type, int num, int id)
    {
        this.type = type;
        this.num = num;
        this.id = id;
    }

    public override string ToString()
    {
        return (int)type + "," + num + "," + id;
    }
}
