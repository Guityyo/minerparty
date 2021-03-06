using UnityEngine;
using System;

public sealed class ChaseThief :  FSMState<Miner> {
	
	static readonly ChaseThief instance = new ChaseThief();
	public static ChaseThief Instance { get { return instance; } }
	static ChaseThief() { }
	private ChaseThief() { }
	
	private Thief thiefScript;
	
	public override void Enter (Miner m) {
		thiefScript = GameObject.Find("Thief").GetComponent<Thief>();
		m.chasing = true;
		m.setTarget ("Thief");
		m.enableSteering (2);
		m.say ("NOOOOO! I HAVE TO CATCH THE THIEF!!!");
	}
	
	public override void Execute (Miner m) {
		m.IncreaseFatigue (2);
		m.IncreaseThirst ();

		if ( m.IsNearTarget (m.MinDistToCatch) ){
			m.disableSteering();
			m.say("MINER: Caught ya!!");
			thiefScript.looseMoney();
			m.endChasing();
			m.ChangeState(QuenchThirstSaloonGetADrink.Instance);
		} else if (m.IsDarkOutside()) {
			thiefScript.notChased();
			m.endChasing();
			m.ChangeState(GoHomeSleep.Instance);
		} else if (m.IsExhausted () ) {
			thiefScript.notChased();
			m.endChasing();
			m.ChangeState (GoHomeSiesta.Instance);
		}
	}
	
	public override void Exit(Miner m) {
		m.say("I have my money back!");
	}
}