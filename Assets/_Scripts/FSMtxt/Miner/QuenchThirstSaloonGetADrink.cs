using UnityEngine;
using System;

public sealed class QuenchThirstSaloonGetADrink :  FSMState<Miner> {
	
	static readonly QuenchThirstSaloonGetADrink instance = new QuenchThirstSaloonGetADrink();
	public static QuenchThirstSaloonGetADrink Instance { get { return instance; } }
	static QuenchThirstSaloonGetADrink() { }
	private QuenchThirstSaloonGetADrink() { }
	
	
	public override void Enter (Miner m) {
		if (m.TargetLocation != Locations.bar) {
			m.say("Hohooo I am thirsy...");
			m.ChangeTargetLocation(Locations.bar);
			m.setTarget("SaloonIdle");
		}
	}
	
	public override void Execute (Miner m) {
		if (m.IsNearTarget (1)){
			m.say("Another pint pleeeeaseeee... ");
			m.disableSteering();

			m.Drinking(200);

			if (m.IsBathroomNeedy()) {
				m.ChangeState (VisitTreeToPee.Instance);
			} else if (m.IsDarkOutside()) {
				m.ChangeState(GoHomeSleep.Instance);
			} else if (m.Thirst <= 0 || m.GoldCarried <= 0) {
				m.ChangeState (ChaseThief.Instance);
			}
		}
	}
	
	public override void Exit(Miner m) {
		m.say("I aaammm reeazdyy nooow tzoo connntinuuue...");
	}
}