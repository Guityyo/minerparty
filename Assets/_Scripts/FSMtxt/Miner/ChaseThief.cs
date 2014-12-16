using UnityEngine;
using System;

public sealed class ChaseThief :  FSMState<Miner> {
	
	static readonly ChaseThief instance = new ChaseThief();
	public static ChaseThief Instance { get { return instance; } }
	static ChaseThief() { }
	private ChaseThief() { }
	
	private Thief thiefScript;
	
	public override void Enter (Miner m) {

		Debug.Log("MINER: NOOOOO! I HAVE TO CATCH THE THIEF!!!");
	
		thiefScript = GameObject.Find("Thief").GetComponent<Thief>();
		m.chasing = true;
		m.setTarget ("Thief");
		m.enableSteering (2);
	}
	
	public override void Execute (Miner m) {

		m.IncreaseFatigue ();
		m.IncreaseThirst ();

		if ( m.IsNearTarget (1) ){
			m.disableSteering();
			m.say("MINER: Caught ya!!");
			m.endChasing();
			thiefScript.looseMoney();
			m.ChangeState(QuenchThirstSaloonGetADrink.Instance);
		} else if (m.IsDarkOutside()) {
			m.endChasing();
			thiefScript.notChased();
			m.ChangeState(GoHomeSleep.Instance);
		} else if (m.IsExhausted () ) {
			m.endChasing();
			thiefScript.notChased();
			m.ChangeState (GoHomeSiesta.Instance);
		}
	}
	
	public override void Exit(Miner m) {
		m.say("I have my money back!");
	}
}