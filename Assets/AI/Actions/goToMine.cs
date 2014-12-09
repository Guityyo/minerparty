 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnitySteer.Behaviors;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class goToMine : RAINAction
{
	private Thief thief;
	private GameObject mine;
	
	public override void Start(RAIN.Core.AI ai) {
		base.Start(ai);
		
		thief = GameObject.Find("Thief").GetComponent<Thief>();
	}
	
	public override ActionResult Execute(RAIN.Core.AI ai)
	{
		thief.setTarget("MineIdle");
		if (! thief.isSteeringEnabled())
			thief.enableSteering(2);
		Debug.Log ("THIEF: Going to the mine to steal some gold...");
		return ActionResult.FAILURE;
	}
	
	public override void Stop(RAIN.Core.AI ai)
	{
		base.Stop(ai);
	}
}