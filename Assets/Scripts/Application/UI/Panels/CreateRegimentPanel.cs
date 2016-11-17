using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateRegimentPanel : UIViewPanel
{

    public InputField inputField;
    public GameObject button;

    protected override void Start()
    {
        base.Start();
        ButtonClickListener.Get(button).onClick = OnButtonClick;
    }

    private void InitInputField()
    {
        inputField.text = "";
    }

    private void OnButtonClick(GameObject go)
    {
        string regimentName = inputField.text;
        if (string.IsNullOrEmpty(regimentName))
        {
            return;
        }
        DataCenterManager.Instance.CreateRegiment(regimentName);
    }
}
