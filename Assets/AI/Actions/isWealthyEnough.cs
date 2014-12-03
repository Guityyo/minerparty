using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class isWealthyEnough : RAINAction
{
	private Thief thief;
    public override void Start(RAIN.Core.AI ai){
        base.Start(ai);
		thief = GameObject.Find("Thief").GetComponent<Thief>();
    }

    public override ActionResult Execute(RAIN.Core.AI ai){
		if(thief.GoldCarried>= Saloon.getBeerPrice())
        	return ActionResult.SUCCESS;
		return ActionResult.FAILURE;
    }

    public override void Stop(RAIN.Core.AI ai){
        base.Stop(ai);
    }
}