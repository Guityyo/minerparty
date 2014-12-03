using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;

public enum Locations { goldmine, bar, bank, home, tree };

[RequireComponent(typeof(SteerToFollow))]
public class Miner : MonoBehaviour {
	private FiniteStateMachine<Miner> FSM;
	
	public Locations TargetLocation = Locations.home;
	public int GoldCarried = 0;
	public int MoneyInBank  = 0;
	public int Thirst = 0;
	public int BathroomNeed = 0;
	public int Fatigue = 0;

	public SteerToFollow minerSteering;
	public Animator animator;

	// Miner position
	public Vector3 minerCurrPos;
	public Vector3 minerTargetPos = new Vector3 ();

	// Target object
	public GameObject minerTarget, mainLight;

	// Output channel
	public GameObject HUD;
	private HudController hudController;

	// To avoid obstacles
	private Vector3 dir;
	public float force = 50.0f;
	public float minimumDistToAvoid = 2.0f;

	private bool gameEnded = false;

	public void AddToGoldCarried(int amount) {
		GoldCarried += amount;
	}
	
	public void AddToMoneyInBank(int amount ) {
		if (GoldCarried >= amount) {
			MoneyInBank += amount;
			GoldCarried -= amount;
		} else {
			MoneyInBank += GoldCarried;
			GoldCarried = 0;
		}
	}
	
	public bool IsRichEnough() {
		return MoneyInBank > 1000;
	}
	
	public bool HasPocketsFull() {
		return GoldCarried >=  250;
	}
	
	public bool IsThirsty() {
		return Thirst >= 3000;
	}
	
	public bool IsBathroomNeedy() {
		return BathroomNeed >= 1000; // our miner has a small bladder
	}

	public bool IsAt(GameObject target) {
		Vector3 targetPos = target.transform.position;
		return Vector3.Distance (minerCurrPos, targetPos) <= 1.0;
	}


	public void Drinking(int amount){
		if (GoldCarried >= Saloon.getBeerPrice()) {
			Thirst -= amount;
			if (Thirst < 0) Thirst = 0;
			BathroomNeed += amount / 2;
			GoldCarried -= Saloon.getBeerPrice();
		}
	}
	
	public void IncreaseFatigue() {
		Fatigue++;
	}

	public bool IsExhausted() {
		return Fatigue >= 250;
	}

	// Method to check if the Miner has arrived to the target
	public bool IsNearTarget(){
		return Vector3.Distance (minerCurrPos, minerTargetPos) <= 1.0;
	}

	// Method to check if it is night
	public bool IsDarkOutside(){
		return mainLight.light.intensity <= 0.4;
	}


	public void ChangeState(FSMState<Miner> e) {
		FSM.ChangeState(e);
	}

	public void say (string words) {
		hudController.setMinerText (words);
		Debug.Log ("Miner: \"" + words + "\"");
	}
	

	public void Awake() {
		minerTarget = GameObject.Find ("Mine");
		mainLight = GameObject.Find ("Main light");
		HUD = GameObject.Find ("HUD");
		hudController = HUD.GetComponent<HudController>();

//		Debug.Log ("Miner awakes...");
		say ("I'm awaking...");

		FSM = new FiniteStateMachine<Miner>();
		FSM.Configure(this, GoHomeSleep.Instance);

		minerSteering = GetComponent<SteerToFollow> ();
		animator = GetComponent<Animator> ();

		//set the target
		minerSteering.Target = minerTarget.transform;

		//enabling steering
		enableSteering(5);

		minerCurrPos = transform.position;
		minerTargetPos = minerSteering.Target.transform.position;

		dir = (minerTargetPos - minerCurrPos); // directional vector to target position
		dir.Normalize();
	}
	
	public void Update() {
		if (IsRichEnough ()) {
			say ("I AM RIIIIIIIICH!!!! YEAAAAAAH!!");
			quitMiningWithMessage ("I AM RIIIIIIIICH!!!! YEAAAAAAH!!");				
		} else {
			Thirst++;
			FSM.Update ();
			minerCurrPos = transform.position;

			// AvoidObstacles method 
			dir = (minerTargetPos - minerCurrPos);
			dir.Normalize();
			AvoidObstacles (ref dir);	

//			if (IsDarkOutside()) ChangeState(GoHomeSleep.Instance); // Just call ChangeState if not in state already -> call from another state
		}
	}


	public void ChangeTargetLocation(Locations l) {
		TargetLocation = l;

//		Debug.Log ("Changing target location to " + l + "...");
		say ("Next stop: " + l + "!");

		if( minerSteering != null){
			enableSteering(5);
		}
	}

	// To set the target of the steering behaviour
	public void setTarget(string newTarget){
		minerSteering = GetComponent<SteerToFollow> ();
		animator = GetComponent<Animator> ();

		minerTarget = GameObject.Find(newTarget);
		minerSteering.Target = minerTarget.transform;

		minerTargetPos = minerSteering.Target.transform.position;
		// for obstacle avoidance
		dir = (minerTargetPos - minerCurrPos); // directional vector to target position
		dir.Normalize();
	}

	// Enable steering behaviour
	public void enableSteering(int vel){
		minerSteering.enabled = true;
		animator.SetInteger ("speed", 200);
	}

	// Disable steering behaviour
	public void disableSteering(){
		minerSteering.enabled = false;
		animator.SetInteger ("speed", 0);
	}
	

	public void quitMiningWithMessage(string message){
		if (!gameEnded) {
			disableSteering();
			Debug.Log (message);
			GUI.Label (new Rect (10, 10, 400, 20), message);
			gameEnded = true;
			Application.Quit (); // it does not work in editor mode
		}
	}
	

	public void AvoidObstacles(ref Vector3 dir)
	{
		RaycastHit hit;
		
		//Only detect layer 8 (Obstacles)
		int layerMask = 1 << 8;
		
		//Check that the character hit with the obstacles within its minimum distance to avoid
		if (Physics.Raycast(transform.position, transform.forward, out hit, minimumDistToAvoid, layerMask))
		{
//			Debug.Log("Obstacle detected, don't wanna run into it!");
			say("Obstacle detected, don't wanna run into it!");

			//Get the normal of the hit point to calculate the new direction
			Vector3 hitNormal = hit.normal;
			hitNormal.y = 0.0f; //Don't want to move in Y-Space
			
			//Get the new directional vector by adding force to vehicle's current forward vector
			dir = transform.forward + hitNormal * force;

			//Rotate the vehicle to its target directional vector
			var rot = Quaternion.LookRotation(dir);
			transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5.0f *  Time.deltaTime);
		}
		
	}
}
