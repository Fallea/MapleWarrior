/// <summary>
/// 创建勇士团请求
/// </summary>
public class CreateRegimentRequest : NetRequest
{
    public CreateRegimentRequest(C2SCMD cmd) : base(cmd) { }

    public string name;

}
