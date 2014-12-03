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
	private GameObject mine;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		thief = GameObject.Find("Thief").GetComponent(Thief);
		mine = GameObject.Find ("MineIdle");
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {

		if (thief.isAt (mine)) {
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