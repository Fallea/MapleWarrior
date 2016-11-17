using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardDetailPanel : UIViewPanel
{

    public Image icon;
    public Image frame;
    public Text nameTxt;
    public Text typeTxt;
    public Text starTxt;
    public Text strengthTxt;
    public Text agilityTxt;
    public Text intellectTxt;

    public Text strIncTxt;
    public Text agiIncTxt;
    public Text intIncTxt;

    public Text hpTxt;
    public Text speedTxt;
    public Text physicalAttackTxt;
    public Text physicalDefenseTxt;
    public Text magicAttackTxt;
    public Text magicDefenseTxt;
    public Text critChanceTxt;
    public Text critDamageTxt;

    private Card card;
    private CardConfigData cardConfigData;

    protected override void Start()
    {
        base.Start();
    }

    public override void Open(object param)
    {
        base.Open(param);
        card = (Card)param;
        cardConfigData = ConfigManager.Instance.GetCardConfigData(card.cardId);
        Display();
    }

    public override void Close()
    {
        base.Close();
        this.card = null;
    }

    private void Display()
    {
        this.frame.sprite = UIManager.Instance.GetCardSprite(cardConfigData.quality.ToString());
        this.nameTxt.text = cardConfigData.name;
        this.typeTxt.text = UIWordUtil.GetCardTypeWord(cardConfigData.type);
        this.starTxt.text = card.star + "星";

        this.strengthTxt.text = card.strength.ToString();
        this.strIncTxt.text = cardConfigData.strInc.ToString();

        this.agilityTxt.text = card.agility.ToString();
        this.agiIncTxt.text = cardConfigData.agiInc.ToString();

        this.intellectTxt.text = card.strength.ToString();
        this.intIncTxt.text = cardConfigData.strInc.ToString();

        this.hpTxt.text = card.hp + "/" + card.hp;
        this.speedTxt.text = card.speed.ToString();
        this.physicalAttackTxt.text = card.physicalAttack.ToString();
        this.physicalDefenseTxt.text = card.physicalDefense.ToString();
        this.magicAttackTxt.text = card.magicAttack.ToString();
        this.magicDefenseTxt.text = card.magicDefense.ToString();
        this.critChanceTxt.text = card.critChance.ToString();
        this.critDamageTxt.text = card.critDamage.ToString();
    }
}
