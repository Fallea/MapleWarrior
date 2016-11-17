using UnityEngine;
using System.Collections.Generic;

public class ServerLogicManager : TSingleton<ServerLogicManager>
{

    ServerLogicManager() { }

    /// <summary>
    /// 生成新的卡牌
    /// </summary>
    /// <returns></returns>
    public SvDataCard NewCard()
    {
        SvDataCard card = new SvDataCard();
        card.id = SvCardIDGenerator.Instance.NewID();

        int index = SvUtil.GetRandom(0, ConfigManager.Instance.GetCardConfigList.Count);
        CopyCardProperties(card, ConfigManager.Instance.GetCardConfigList[index]);

        return card;
    }

    /// <summary>
    /// 拷贝卡牌属性
    /// </summary>
    /// <param name="card"></param>
    /// <param name="cardConfigData"></param>
    private void CopyCardProperties(SvDataCard card, CardConfigData cardConfigData)
    {
        card.cardId = cardConfigData.id;

        card.strength = cardConfigData.strength;
        card.agility = cardConfigData.agility;
        card.intellect = cardConfigData.intellect;

        card.hp = cardConfigData.hp;
        card.physicalAttack = cardConfigData.physicalAttack;
        card.physicalDefense = cardConfigData.physicalDefense;
        card.magicAttack = cardConfigData.magicAttack;
        card.magicDefense = cardConfigData.magicDefense;
        card.speed = cardConfigData.speed;
        card.critChance = cardConfigData.critChance;
        card.critDamage = cardConfigData.critDamage;
    }

	//================================================================

	public void PowerCutTime(SvPlayer svPlayer)
	{


	}
}
