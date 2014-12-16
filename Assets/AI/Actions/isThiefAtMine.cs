using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
//TODO change name to isThiefNotAtMine
public class isThiefAtMine : RAINAction
{
	private Thief thief;
	
	public override void Start(RAIN.Core.AI ai) {
		base.Start(ai);
		thief = GameObject.Find("Thief").GetComponent<Thief>();
	}
	
	public override ActionResult Execute(RAIN.Core.AI ai) {
		thief.setTarget ("MineIdle");
		if (thief.IsNearTarget(7)) {
			thief.disableSteering();
			thief.enableWandering(2);
			return ActionResult.SUCCESS;
		}
		Debug.Log ("THIEF: I should probably head to the mine then...");
		return ActionResult.FAILURE;
	}
	
	public override void Stop(RAIN.Core.AI ai)
	{
		base.Stop(ai);
	}
}