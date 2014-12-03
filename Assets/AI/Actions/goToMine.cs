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

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);

		thief = GameObject.Find("Thief").GetComponent(Thief);
		mine = GameObject.Find ("MineIdle");

		thief.setTarget(mine);

    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		if(!thief.isSteeringEnabled)
			thief.enableSteering();

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}