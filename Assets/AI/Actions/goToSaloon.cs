using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnitySteer.Behaviors;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class goToSaloon : RAINAction
{
	private SteerToFollow thiefSteering;
	private Thief thief;
	private GameObject saloon;
	
	public override void Start(RAIN.Core.AI ai)
	{
		base.Start(ai);
		
		thief = GameObject.Find("Thief").GetComponent<Thief>();

		
	}
	
	public override ActionResult Execute(RAIN.Core.AI ai)
	{
		thief.setTarget("SaloonIdle");
		if (! thief.isSteeringEnabled())
			thief.enableSteering(1);
		Debug.Log ("THIEF: Going to the saloon to drink...");
		return ActionResult.FAILURE;
	}
    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}