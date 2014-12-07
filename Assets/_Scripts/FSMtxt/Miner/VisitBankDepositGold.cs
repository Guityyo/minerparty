using UnityEngine;
using System;

public sealed class VisitBankDepositGold :  FSMState<Miner> {
	
	static readonly VisitBankDepositGold instance = new VisitBankDepositGold();
	public static VisitBankDepositGold Instance { get { return instance; } }
	static VisitBankDepositGold() { }
	private VisitBankDepositGold() { }
	
		
	public override void Enter (Miner m) {
		// Looking at our FSM, our miner can only go to the saloon when he's at the mine
		//		if (m.Thirsty()) {
		//			m.ChangeState(QuenchThirstSaloon.Instance);
		//		} else 
		if(m.TargetLocation != Locations.bank) {
			m.say("Entering the bank...");
			m.ChangeTargetLocation(Locations.bank);;
			m.setTarget("BankIdle");
		}
	}
	
	public override void Execute (Miner m) {
		// Looking at our FSM, our miner can only go to the saloon when he's at the mine
		//		if (m.Thirsty()) {
		//			m.ChangeState(QuenchThirstSaloon.Instance);
		//		}

		if ( m.IsNearTarget () ){
			m.say("Feeding The System with MY gold " + m.MoneyInBank + "...");
			m.disableSteering();
			m.AddToMoneyInBank(25);
			if (m.IsDarkOutside()) {
				m.ChangeState(GoHomeSleep.Instance);
			} else if (m.GoldCarried <= 0)	{
				m.ChangeState(ChaseThief.Instance);
			}
		}
	}
	
	public override void Exit(Miner m) {
		m.say("Leaving the bank...");
	}
}