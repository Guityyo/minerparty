using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class drinkTogether : RAINAction
{
	private Miner minerScript;
	private Thief thiefScript;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		minerScript = GameObject.Find ("Miner").GetComponent<Miner>();
		thiefScript = GameObject.Find("Thief").GetComponent<Thief>();
		thiefScript.disableAllMovement();
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		minerScript.BuyDrinks(1);
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}