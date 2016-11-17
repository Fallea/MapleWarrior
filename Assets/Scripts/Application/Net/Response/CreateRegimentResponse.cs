using UnityEngine;
using System.Collections;

public class CreateRegimentResponse : NetResponse
{
    public CreateRegimentResponse(S2CCMD cmd) : base(cmd) { }

	public bool isCreated;
}
