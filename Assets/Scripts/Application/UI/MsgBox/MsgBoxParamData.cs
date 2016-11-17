
public class MsgBoxParamData
{
    public string msg;

    public System.Action<MsgBoxEvent, object> callback = null;

    public MsgBoxType type;

    public object param;

    public void Clear()
    {
        msg = null;
        callback = null;
    }
}