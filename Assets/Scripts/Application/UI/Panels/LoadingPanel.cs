using UnityEngine;
using System.Collections;

public class LoadingPanel : UIViewPanel
{

    void Awake()
    {
        UIManager.Instance.SetLoadingPanel(this);
        Close();
    }
}
