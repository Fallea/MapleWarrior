using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageBoxPanel : UIViewPanel
{

    public Text msgText;
    public GameObject cancelBtn;
    public GameObject okBtn;
    public GameObject centerOkBtn;

    public MsgBoxParamData paramData = null;

    protected override void Start()
    {
        base.Start();
        ButtonClickListener.Get(cancelBtn).onClick = OnCancelButtonClick;
        ButtonClickListener.Get(okBtn).onClick = OnOkButtonClick;
        ButtonClickListener.Get(centerOkBtn).onClick = OnOkButtonClick;
    }

    private void OnCancelButtonClick(GameObject go)
    {
        try
        {
            if (paramData != null && paramData.callback != null)
            {
                paramData.callback.Invoke(MsgBoxEvent.Cancel, paramData.param);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        UIManager.Instance.Close(UIPanelUtil.MessageBoxPanel);
    }

    private void OnOkButtonClick(GameObject go)
    {
        try
        {
            if (paramData != null && paramData.callback != null)
            {
                paramData.callback.Invoke(MsgBoxEvent.Ok, paramData.param);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        UIManager.Instance.Close(UIPanelUtil.MessageBoxPanel);
    }

    public override void Close()
    {
        base.Close();
        if (paramData != null)
        {
            paramData.Clear();
        }
        paramData = null;
    }

    public override void Open(object param)
    {
        base.Open(param);

        paramData = (MsgBoxParamData)param;
        SwitchButton(paramData.type == MsgBoxType.Ok);
        msgText.text = paramData.msg;
    }

    private void SwitchButton(bool isSingleOkBtn)
    {
        cancelBtn.SetActive(!isSingleOkBtn);
        okBtn.SetActive(!isSingleOkBtn);
        centerOkBtn.SetActive(isSingleOkBtn);
    }
}
