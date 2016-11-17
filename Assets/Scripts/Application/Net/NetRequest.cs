
/// <summary>
/// 数据请求基类
/// </summary>
public class NetRequest
{
    private C2SCMD mCmd;

    protected NetRequest() { }

    public NetRequest(C2SCMD cmd)
    {
        this.mCmd = cmd;
    }

    public C2SCMD cmd
    {
        get { return this.mCmd; }
    }
}