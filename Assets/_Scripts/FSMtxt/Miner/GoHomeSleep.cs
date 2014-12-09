using UnityEngine;
using System;

public sealed class GoHomeSleep :  FSMState<Miner> {
	
	static readonly GoHomeSleep instance = new GoHomeSleep();
	public static GoHomeSleep Instance { get { return instance; } }
	static GoHomeSleep() { }
	private GoHomeSleep() { }
	
	
	public override void Enter (Miner m) {
		if (m.TargetLocation != Locations.home) {
			m.say("Night time comes - off to home sweet home...");
			m.ChangeTargetLocation(Locations.home);
			m.setTarget("HomeIdle");
		}
	}
	
	public override void Execute (Miner m) {
		if (m.IsNearTarget (1)){
			m.say("zZzZzZz ... good night!!");
			m.disableSteering();
			if (m.Fatigue > 0) m.Fatigue--;

			if (! m.IsDarkOutside()){
				m.say("I see the sun rising!!");
				m.ChangeState(EnterMineDigForGold.Instance);
			}
		}
	}
	
	public override void Exit(Miner m) {
		m.say("Don't wanna go to work, it's too early...");
	}
}