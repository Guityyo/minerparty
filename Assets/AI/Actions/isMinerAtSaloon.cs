using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class isMinerAtSaloon : RAINAction
{
	private Miner miner;
	private GameObject saloon;
	
	public override void Start(RAIN.Core.AI ai) {  
		base.Start(ai);
		miner = GameObject.Find ("Miner").GetComponent<Miner>();
		saloon = GameObject.Find ("Saloon");
	}
	
	public override ActionResult Execute(RAIN.Core.AI ai) {
		return miner.IsAt(saloon) ? ActionResult.SUCCESS : ActionResult.FAILURE;
	}

    public override void Stop(RAIN.Core.AI ai) {
        base.Stop(ai);
    }
}