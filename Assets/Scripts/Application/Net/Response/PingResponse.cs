
public class PingResponse : NetResponse 
{
	public PingResponse(S2CCMD cmd) : base(cmd) { }

	public long time;
	public long lastPowerTime;
}
