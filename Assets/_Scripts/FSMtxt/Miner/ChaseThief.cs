using UnityEngine;
using System;

public sealed class ChaseThief :  FSMState<Miner> {
	
	static readonly ChaseThief instance = new ChaseThief();
	public static ChaseThief Instance { get { return instance; } }
	static ChaseThief() { }
	private ChaseThief() { }
	
	
	
	public override void Enter (Miner m) {

		Debug.Log("MINER: NOOOOO! I HAVE TO CATCH THE THIEF!!!");
	
		m.setTarget ("Thief");
		m.enableSteering (400);
	}
	
	public override void Execute (Miner m) {
		
		if ( m.IsNearTarget () ){
			m.disableSteering();
			m.say("MINER: Caught ya!!");
			m.IncreaseFatigue ();

			m.ChangeState(QuenchThirstSaloonGetADrink.Instance);
		}
	}
	
	public override void Exit(Miner m) {
		m.say("Leaving the mine...");
	}
}