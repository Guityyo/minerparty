using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class isMineVisible : RAINAction
{
	private Perspective thiefPerspective;
	private GameObject mine;
    
	public override void Start(RAIN.Core.AI ai) {
        base.Start(ai);
		thiefPerspective = GameObject.Find("Thief").GetComponent<Perspective>() ;
		mine = GameObject.Find("Mine");
    }

    public override ActionResult Execute(RAIN.Core.AI ai) {
		if (thiefPerspective.IsWithinViewfield (mine)) {
			Debug.Log("Mine Visible!!");
			return ActionResult.SUCCESS;
		}
		Debug.Log("Mine not visible :(");
		return ActionResult.FAILURE;
    }

    public override void Stop(RAIN.Core.AI ai) {
        base.Stop(ai);
    }
}