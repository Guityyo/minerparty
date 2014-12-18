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
		Debug.Log ("THIEF: Going to the mine to steal some gold...");
		thief.setTarget("MineIdle");
		thief.enableSteering(2);
		return ActionResult.SUCCESS;
	}
	
	public override void Stop(RAIN.Core.AI ai)
	{
		base.Stop(ai);
	}
}