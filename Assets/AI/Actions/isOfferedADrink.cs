using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class isOfferedADrink : RAINAction
{
	private Miner minerScript;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		minerScript = GameObject.Find ("Miner").GetComponent<Miner> ();
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		return (minerScript.IsRichEnough()) ? ActionResult.SUCCESS : ActionResult.FAILURE;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}