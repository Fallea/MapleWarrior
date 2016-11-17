
/// <summary>
/// 错误提示处理管理中心
/// </summary>
public class ErrorTipsManager : TSingleton<ErrorTipsManager>
{
    ErrorTipsManager() { }

    public void Handle(int error)
    {
        UIManager.Instance.MsgBox("Error:" + error);
    }
}