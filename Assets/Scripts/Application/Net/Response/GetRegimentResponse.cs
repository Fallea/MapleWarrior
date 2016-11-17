using UnityEngine;
using System.Collections;

public class GetRegimentResponse : NetResponse
{
    public GetRegimentResponse(S2CCMD cmd) : base(cmd) { }

    public SvDataPlayer player = null;
	public long serverTime; 

	public int levelMax;
	public int cardLvMax;
	public int cardNumMax;
	public int powerTime;

}
