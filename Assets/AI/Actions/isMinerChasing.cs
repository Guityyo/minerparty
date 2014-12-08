using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class isMinerChasing : RAINAction
{
	private Miner miner;
	
	public override void Start(RAIN.Core.AI ai)
	{
		base.Start(ai);
		miner = GameObject.Find("Miner").GetComponent<Miner>();
	}
	
	public override ActionResult Execute(RAIN.Core.AI ai)
	{
		Debug.Log ("THIEF: ruuuuuuuuuuuuun!!!!!");
		if (miner.isChasing())
			return ActionResult.SUCCESS;
		return ActionResult.FAILURE;
	}
	
	public override void Stop(RAIN.Core.AI ai)
	{
		base.Stop(ai);
	}
}