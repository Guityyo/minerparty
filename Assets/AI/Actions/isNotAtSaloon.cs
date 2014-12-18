using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class isNotAtSaloon : RAINAction
{
	private Thief thief;
	private Vector3 saloonPos;
	
	public override void Start(RAIN.Core.AI ai) {
		base.Start(ai);
		thief = GameObject.Find("Thief").GetComponent<Thief>();
		saloonPos = GameObject.Find("SaloonIdle").transform.position;
	}
	
	public override ActionResult Execute(RAIN.Core.AI ai){
		Debug.Log ("THIEF: Let's go to the saloon...");
		return thief.IsNear(5, saloonPos) ? ActionResult.FAILURE : ActionResult.SUCCESS;
	}

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}