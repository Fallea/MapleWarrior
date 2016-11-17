/// <summary>
/// 请求勇士团数据
/// </summary>
public class GetRegimentRequest : NetRequest
{
    public GetRegimentRequest(C2SCMD cmd) : base(cmd) { }
	
    public int sessionId;

}
