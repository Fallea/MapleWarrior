
public class CardLotteryResponse : NetResponse
{
    public CardLotteryResponse(S2CCMD cmd) : base(cmd) { }

    public SvDataCard card;
}
