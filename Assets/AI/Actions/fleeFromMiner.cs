using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class fleeFromMiner : RAINAction
{
	private Thief thiefScript;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		thiefScript = GameObject.Find("Thief").GetComponent<Thief>();
	}

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		thiefScript.enableEvasion ();
        return ActionResult.FAILURE;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}