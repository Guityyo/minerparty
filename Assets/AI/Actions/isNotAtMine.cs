using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class isNotAtMine : RAINAction
{
	private Thief thief;
	private Vector3 minePostion;
	
	public override void Start(RAIN.Core.AI ai) {
		base.Start(ai);
		thief = GameObject.Find("Thief").GetComponent<Thief>();
		minePostion = GameObject.Find("MineIdle").transform.position;
	}
	
	public override ActionResult Execute(RAIN.Core.AI ai) {
		return (thief.IsNear(7, minePostion)) ? ActionResult.FAILURE : ActionResult.SUCCESS;
	}
	
	public override void Stop(RAIN.Core.AI ai)
	{
		base.Stop(ai);
	}
}