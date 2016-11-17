/// <summary>
/// 客服端发送到服务端的命令号
/// </summary>
public enum C2SCMD : int
{
	/// <summary>
	/// 心跳
	/// </summary>
	Ping = 1,
    /// <summary>
    /// 客服端发送登录命令号
    /// </summary>
    Login,
    /// <summary>
    /// 勇士团数据
    /// </summary>
    GetRegiment,
    /// <summary>
    /// 创建勇士团
    /// </summary>
    CreateRegiment,

    /// <summary>
    /// 卡牌抽奖
    /// </summary>
    CardLottery,
}

/// <summary>
/// 服务端发送到客服端的命令号
/// </summary>
public enum S2CCMD : int
{
	/// <summary>
	/// 心跳
	/// </summary>
	Ping = 1,
    /// <summary>
    /// 服务器发送登录命令号
    /// </summary>
    Login,
    /// <summary>
    /// 请求勇士团数据
    /// </summary>
    GetRegiment,
    /// <summary>
    /// 创建勇士团
    /// </summary>
    CreateRegiment,

    /// <summary>
    /// 卡牌抽奖
    /// </summary>
    CardLottery,

	/// <summary>
	/// 行动力推送
	/// </summary>
	PushPower = 1000,
}