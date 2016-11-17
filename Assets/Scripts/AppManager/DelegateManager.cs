using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 委托事件处理中心
/// </summary>
public class DelegateManager : TSingleton<DelegateManager>
{
    DelegateManager() { }

    private Dictionary<DelegateCommand, System.Action> voidActions = new Dictionary<DelegateCommand, System.Action>();
    private Dictionary<DelegateCommand, System.Action<object>> objectActions = new Dictionary<DelegateCommand, System.Action<object>>();

    public void Add(DelegateCommand command, System.Action callback)
    {
        if (voidActions.ContainsKey(command))
        {
            voidActions[command] += callback;
        }
        else
        {
            voidActions.Add(command, callback);
        }
    }

    public void Add(DelegateCommand command, System.Action<object> callback)
    {
        if (objectActions.ContainsKey(command))
        {
            objectActions[command] += callback;
        }
        else
        {
            objectActions.Add(command, callback);
        }
    }

    public void Send(DelegateCommand command)
    {
        if (voidActions.ContainsKey(command))
        {
            voidActions[command].Invoke();
        }
    }

    public void Send(DelegateCommand command, object obj)
    {
        if (objectActions.ContainsKey(command))
        {
            objectActions[command].Invoke(obj);
        }
    }

    public void Remove(DelegateCommand command)
    {
        if (voidActions.ContainsKey(command))
        {
            voidActions[command] = null;
            voidActions.Remove(command);
        }
        if (objectActions.ContainsKey(command))
        {
            objectActions[command] = null;
            objectActions.Remove(command);
        }
    }
}
