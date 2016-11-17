
/// <summary>
/// 数据相应基类
/// </summary>
public class NetResponse {

	private S2CCMD mCmd;

    protected NetResponse(){}

    public NetResponse(S2CCMD cmd)
    {
        this.mCmd = cmd;
    }

    public S2CCMD cmd
    {
        get { return this.mCmd; }
    }

	public int error;
}
