
/// <summary>
/// 使用词语定义在该处
/// </summary>
public class UIWordUtil
{
    private static string[] CardTypeWords = new string[] { "", "战士", "刺客", "法师", "弓手", "枪手" };

    public static string GetCardTypeWord(CardType type)
    {
        return CardTypeWords[(int)type];
    }
}