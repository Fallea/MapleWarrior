
public class SvPlayer
{
    public SvDataPlayer svDataPlayer;

	/// <summary>
	/// 是否有更新
	/// </summary>
	public bool updated;

    /// <summary>
    /// 添加新的卡牌
    /// </summary>
    /// <param name="card"></param>
    public void AddNewCard(SvDataCard card)
    {
        svDataPlayer.cards.Add(card);
		updated = true;
    }

	public void AddPower(int num)
	{
		svDataPlayer.power += num;
		if (svDataPlayer.power > svDataPlayer.powerMax) {
			svDataPlayer.power = svDataPlayer.powerMax;
		}
		updated = true;
	}


}
