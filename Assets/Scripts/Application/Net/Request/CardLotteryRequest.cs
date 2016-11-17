/// <summary>
/// 卡牌抽奖
/// </summary>
public class CardLotteryRequest : NetRequest
{
    public CardLotteryRequest(C2SCMD cmd) : base(cmd) { }

    public int type;

}
