using UnityEngine;
using System;

public sealed class VisitTreeToPee :  FSMState<Miner> {
	
	static readonly VisitTreeToPee instance = new VisitTreeToPee();
	public static VisitTreeToPee Instance { get { return instance; } }
	static VisitTreeToPee() { }
	private VisitTreeToPee() { }
	
	
	public override void Enter (Miner m) {
		if (m.TargetLocation != Locations.tree) {
			m.say("Woooo, I need to run to a bathroom..or tree.. whatever is closer...");
			m.ChangeTargetLocation(Locations.tree);
			m.setTarget("TreeIdle");
		}
	}
	
	public override void Execute (Miner m) {
		if (m.IsNearTarget (1)){
			m.say("Oooh, that feels good!!");
			m.disableSteering();
			m.BathroomNeed -= 20;
			m.Thirst -= 1; // compensate geting thirsty while peeing

			if (m.BathroomNeed <= 0){
				m.say("Alright, now I can drink again!!");
				m.ChangeState(QuenchThirstSaloonGetADrink.Instance);
			}
		}
	}
	
	public override void Exit(Miner m) {
		m.say("Hm, cannot wash my hands out here.. damn it...");
	}
}