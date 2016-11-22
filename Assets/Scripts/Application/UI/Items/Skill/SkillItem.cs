using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillItem : UIViewItem
{
    public Image icon;
    public Image frame;

    public ViewItemDelegate onItemClick;

    protected override void OnStart()
    {
        base.OnStart();
        ButtonClickListener.Get(this.gameObject).onClick = OnClick;
    }

    protected override void OnUpdateData()
    {
        SkillConfigData skill = ConfigManager.Instance.GetSkillConfigData((int)this.data);
        icon.sprite = UIManager.Instance.GetSkillSprite(skill.id.ToString());
    }

    private void OnClick(GameObject go)
    {
        if (onItemClick != null)
        {
            onItemClick.Invoke(this);
        }
    }

}
