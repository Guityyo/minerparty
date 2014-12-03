using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class isNotAtSaloon : RAINAction
{
	private Thief thief;
	private GameObject saloon;
	
	public override void Start(RAIN.Core.AI ai)
	{
		base.Start(ai);
		thief = GameObject.Find("Thief").GetComponent(Thief);
		saloon = GameObject.Find ("SaloonIdle");
	}
	
	public override ActionResult Execute(RAIN.Core.AI ai)
	{
		
		if (thief.isAt (saloon)) {
			thief.disableSteering();
			return ActionResult.SUCCESS;
		}
		return ActionResult.FAILURE;
	}

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}