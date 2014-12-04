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
		mine = GameObject.Find ("MineIdle");
    }

    public override ActionResult Execute(RAIN.Core.AI ai) {
		if (miner.IsAt (mine)) {
			Debug.Log("THIEF: The miner is now in the mine... I can go there too!");
			return ActionResult.SUCCESS;
		}
		return  ActionResult.FAILURE;
    }

    public override void Stop(RAIN.Core.AI ai) {
        base.Stop(ai);
    }
}