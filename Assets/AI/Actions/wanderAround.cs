using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class wanderAround : RAINAction
{
	private Thief thief;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		thief = GameObject.Find("Thief").GetComponent<Thief>();
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		thief.enableWandering(1);
		return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
	{
        base.Stop(ai);
    }
}