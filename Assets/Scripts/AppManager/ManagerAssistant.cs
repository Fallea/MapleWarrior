using UnityEngine;
using System.Collections;


/// 管理器助手类
public class ManagerAssistant : TSingleton<ManagerAssistant>
{
    private ManagerAssistant() { }

    private GameObject assistant = null;

    public void Initialize()
    {
        if (assistant == null)
        {
            assistant = new GameObject("ManagerAssistant");
            GameObject.DontDestroyOnLoad(assistant);
        }

        ResourceUpdateManager.Instance.Initialize(assistant);
        ResourceLoadManager.Instance.Initialize(assistant);
    }

}
