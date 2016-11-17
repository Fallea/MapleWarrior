using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardItem : UIViewItem 
{
	public Image icon;
	public Image frame;
	public Text nameTxt;
	public Text starTxt;
	public Text typeTxt;

	public ViewItemDelegate onItemClick;

	protected override void OnStart ()
	{
		base.OnStart ();
		ButtonClickListener.Get (this.gameObject).onClick = OnClick;
	}
	
	protected override void OnUpdateData()
	{
		Card card = (Card)this.data;
		CardConfigData cardConfigData = ConfigManager.Instance.GetCardConfigData(card.cardId);
		this.nameTxt.text = cardConfigData.name;
		this.starTxt.text = card.star + "星";
		this.typeTxt.text = UIWordUtil.GetCardTypeWord(cardConfigData.type);
		this.frame.sprite = UIManager.Instance.GetCardSprite (cardConfigData.quality.ToString());
	}

	private void OnClick(GameObject go)
	{
		if (onItemClick != null) 
		{
			onItemClick.Invoke(this);
		}
	}

}
