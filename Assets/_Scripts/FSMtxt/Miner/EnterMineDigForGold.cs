using UnityEngine;
using System;

public sealed class EnterMineDigForGold :  FSMState<Miner> {
	
	static readonly EnterMineDigForGold instance = new EnterMineDigForGold();
	public static EnterMineDigForGold Instance { get { return instance; } }
	static EnterMineDigForGold() { }
	private EnterMineDigForGold() { }



	public override void Enter (Miner m) {
	
		if (m.TargetLocation != Locations.goldmine) {
			m.say("Heading to the mine...");
			m.ChangeTargetLocation(Locations.goldmine);
			m.setTarget("MineIdle");
		}
	}
	
	public override void Execute (Miner m) {

		if ( m.IsNearTarget () ){
			m.disableSteering();
			m.AddToGoldCarried (Mathf.RoundToInt(1 + UnityEngine.Random.value * 3)); // Each time, gets something between 1 and 4 gold!
			m.say("Picking up nugget and that's " + m.GoldCarried + "...");
			m.IncreaseFatigue ();

			if (m.IsDarkOutside()) {
				m.ChangeState(GoHomeSleep.Instance);
			} else if (m.HasPocketsFull ()) {
				m.ChangeState (VisitBankDepositGold.Instance);
			} else if (m.IsThirsty () && m.GoldCarried >= 50) { // so that our miner can afford some drinks
				m.ChangeState (QuenchThirstSaloonGetADrink.Instance);
				m.enableSteering(7);
			} else if (m.IsExhausted () ) {
				m.ChangeState (GoHomeSiesta.Instance);
			}
		}
	}
	
	public override void Exit(Miner m) {
		m.say("Leaving the mine...");
	}
}