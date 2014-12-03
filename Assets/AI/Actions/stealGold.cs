using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class stealGold : RAINAction
{
	Miner minerScript;
	Thief thiefScript;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		minerScript = GameObject.Find("Miner").GetComponent<Miner>();
		minerScript = GameObject.Find("Thief").GetComponent<Thief>();
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		thiefScript.stealMoneyFromMiner();
		return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}