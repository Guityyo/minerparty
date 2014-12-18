using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class isAtMine : isNotAtMine
{
    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		return (base.Execute(ai) == ActionResult.SUCCESS) ? ActionResult.FAILURE : ActionResult.SUCCESS;
    }
}