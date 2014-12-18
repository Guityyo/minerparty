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
		thief = GameObject.Find("Thief").GetComponent<Thief>();
		thief.moneyStolen = false;
		thief.setTarget ("SaloonIdle");
	}
	
	public override ActionResult Execute(RAIN.Core.AI ai){

		if (thief.IsNearTarget(3)) {
			thief.disableSteering();
			thief.disableWandering();
			return ActionResult.SUCCESS;
		}
		Debug.Log ("THIEF: Let's go to the saloon...");
		return ActionResult.FAILURE;
	}

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}