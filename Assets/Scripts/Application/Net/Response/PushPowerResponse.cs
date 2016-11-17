
public class PushPowerResponse : NetResponse 
{
	public PushPowerResponse(S2CCMD cmd) : base(cmd) { }

	public long serverTime;
	public long lastPowerTime;
	public int power;
	public int powerMax;
}
