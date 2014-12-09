using UnityEngine;
using System;

public sealed class TakeThiefForADrink :  FSMState<Miner> {
	
	static readonly TakeThiefForADrink instance = new TakeThiefForADrink();
	public static TakeThiefForADrink Instance { get { return instance; } }
	static TakeThiefForADrink() { }
	private TakeThiefForADrink() { }
	
	
	public override void Enter (Miner m) {
		if (m.TargetLocation != Locations.bar) {
			m.say("I caught you, let's go for a drink...");
			m.ChangeTargetLocation(Locations.bar);
			m.setTarget("SaloonIdle");
		}
	}
	
	public override void Execute (Miner m) {
		if (m.IsNearTarget (1)){
			m.say("Two pints for da both of us pleeeeaseeee... ");
			m.disableSteering();

			m.Drinking(100);
			m.GoldCarried--; // Give a drink to the thief

			if (m.IsDarkOutside()) {
				m.ChangeState(GoHomeSleep.Instance);
			} else if (m.Thirst <= 0 || m.GoldCarried <= 0) {
				m.ChangeState (ChaseThief.Instance);
			}
		}
	}
	
	public override void Exit(Miner m) {
		m.say("Bye miss thief, I aaammm reeazdyy nooow tzoo connntinuuue...");
	}
}