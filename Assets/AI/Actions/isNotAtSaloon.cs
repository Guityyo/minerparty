using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class isNotAtSaloon : RAINAction
{
	private Thief thief;
	
	public override void Start(RAIN.Core.AI ai) {
		base.Start(ai);
	}
	
	public override ActionResult Execute(RAIN.Core.AI ai){
		Debug.Log ("THIEF: Let's go to the saloon...");
		return thief.IsNearTarget(3) ? ActionResult.SUCCESS : ActionResult.FAILURE;
	}

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}