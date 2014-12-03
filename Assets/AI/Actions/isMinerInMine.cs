using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class isMinerInMine : RAINAction
{
	private Miner miner;
	private GameObject mine;

    public override void Start(RAIN.Core.AI ai) {  
		base.Start(ai);
		miner = GameObject.Find ("Miner").GetComponent<Miner>();
		mine = GameObject.Find ("Mine");
    }

    public override ActionResult Execute(RAIN.Core.AI ai) {
		return miner.IsAt(mine) ? ActionResult.SUCCESS : ActionResult.FAILURE;
    }

    public override void Stop(RAIN.Core.AI ai) {
        base.Stop(ai);
    }
}