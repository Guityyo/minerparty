using UnityEngine;
using System;

public sealed class GoHomeSiesta :  FSMState<Miner> {
	
	static readonly GoHomeSiesta instance = new GoHomeSiesta();
	public static GoHomeSiesta Instance { get { return instance; } }
	static GoHomeSiesta() { }
	private GoHomeSiesta() { }
	
	
	public override void Enter (Miner m) {
		if (m.TargetLocation != Locations.home) {
			m.say("Yawn.. time for a nap, off to home sweet home...");
			m.ChangeTargetLocation(Locations.home);
			m.setTarget("HomeIdle");
		}
	}
	
	public override void Execute (Miner m) {
		if (m.IsNearTarget (1)){
			m.say("zZzZzZz ... I like siesta... ");
			m.disableSteering();
			m.Fatigue--;

			if (m.IsDarkOutside()) {
				m.say("Now that it's dark I might just stay in bed...");
				m.ChangeState(GoHomeSleep.Instance);
			} else if (m.Fatigue <= 0){
				m.ChangeState(EnterMineDigForGold.Instance);
			}
		}
	}
	
	public override void Exit(Miner m) {
		m.say("I don't want to leave homeeee, that nap wasn't long enough...");
	}
}