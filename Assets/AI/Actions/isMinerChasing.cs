using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class isMinerChasing : RAINAction
{
	private Miner minerScript;
	private Thief thiefScript;

	public override void Start(RAIN.Core.AI ai)
	{
		base.Start(ai);

		minerScript = GameObject.Find("Miner").GetComponent<Miner>();
		thiefScript = GameObject.Find("Thief").GetComponent<Thief>();
	}
	
	public override ActionResult Execute(RAIN.Core.AI ai)
	{
		Debug.Log ("THIEF: ruuuuuuuuuuuuun!!!!!    Miner is chasing "+minerScript.IsChasing());
	

		if (minerScript.IsChasing())
			return ActionResult.FAILURE;

		thiefScript.disableEvasion ();
		return ActionResult.SUCCESS;
	}
	
	public override void Stop(RAIN.Core.AI ai)
	{
		base.Stop(ai);
	}
}