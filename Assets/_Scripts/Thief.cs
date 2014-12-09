using UnityEngine;
using UnitySteer.Behaviors;
using System.Collections;

[RequireComponent(typeof(SteerForEvasion))]
[RequireComponent(typeof(SteerToFollow))]
[RequireComponent(typeof(Wander))]
public class Thief : MonoBehaviour {
	Perspective perspective;
	private SteerForEvasion thiefEvasion;
	private SteerToFollow thiefSteering;
	private Wander thiefWander;
	private Animator animator;
	public Vector3 thiefCurrPos;
	public Vector3 thiefTargetPos = new Vector3 ();
	public GameObject thiefTarget;
	public int GoldCarried = 0;
	private Miner minerScript;
	public bool moneyStolen = false;
	
	private Vector3 dir;
	//TODO add to avoid obstacles ??
	
	// Use this for initialization
	void Start () {
		
		perspective = gameObject.GetComponent<Perspective>();
		thiefSteering = GetComponent<SteerToFollow> ();
		thiefWander = GetComponent<Wander> ();
		animator = GetComponent<Animator> ();
		minerScript = GameObject.Find("Miner").GetComponent<Miner>();
		thiefCurrPos = transform.position;
		
		thiefSteering.enabled = false;
		thiefWander.enabled = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject mine = GameObject.Find("Mine");
		if (perspective.IsWithinViewfield (mine))
			Debug.Log ("jippie!");
		
		thiefCurrPos = transform.position;
	}
	
	// Method to check if the Thief has arrived to the target
	public bool IsNearTarget(){
		return Vector3.Distance (thiefCurrPos, thiefTargetPos) <= 3.0;
	}
	
	// To set the target of the steering behaviour
	public void setTarget(string newTarget){
		//if (!thiefTarget.Equals(newTarget)) { //TODO this doesn't work, should be fixed
		thiefSteering = GetComponent<SteerToFollow> ();
		//animator = GetComponent<Animator> ();
		
		thiefTarget = GameObject.Find(newTarget);
		thiefSteering.Target = thiefTarget.transform;   // assign better the target
		thiefTargetPos = thiefSteering.Target.transform.position;
		
		// for obstacle avoidance
		dir = (thiefTargetPos - thiefCurrPos); // directional vector to target position
		dir.Normalize();
		//}
	}
	
	// Enable steering behaviour and disable wandering behaviour
	public void enableSteering(int vel){ 
		thiefSteering.enabled = true;
		thiefWander.enabled = false;
		animator.SetInteger ("speed", vel);
	}
	
	// Disable steering behaviour and enable wandering behaviour
	public void disableSteering(){
		thiefWander.enabled = true;
		thiefSteering.enabled = false;
		animator.SetInteger ("speed", 1);
	}

	// Enable wander behaviour and disable Idle animator
	public void enableWandering(){
		thiefWander.enabled = true;
		animator.SetInteger ("speed", 1);
	}
	// Disable wander behaviour and enable Idle animator
	public void disableWandering(){
		thiefWander.enabled = false;
		animator.SetInteger ("speed", 1);
	}
	
	public bool isAt(GameObject location){
		return Vector3.Distance (thiefCurrPos, location.transform.position) <= 1.0;
	}
	
	// To check if steering is enabled
	public bool isSteeringEnabled(){
		return thiefSteering.enabled;
	}
	
	public void stealMoneyFromMiner() {
		Debug.Log("THIEF: Stealing...");
		int goldToSteal = minerScript.GoldCarried;
		minerScript.GoldCarried -= goldToSteal;
		GoldCarried += goldToSteal;
		moneyStolen = true;
		minerScript.chasing = true;
		Debug.Log("THIEF: I'm soo good, I just stole " + GoldCarried + " gold!");
	}

	public void looseMoney() {
		Debug.Log("THIEF: Oh no! I didn't want to steal you...");
		int goldToSteal = GoldCarried;
		GoldCarried -= goldToSteal;
		minerScript.GoldCarried += goldToSteal;
		moneyStolen = false;
	}
	
	public bool hasStolenMoney(){
		return moneyStolen;
	}
	
	public void drinkBeer(){
		GoldCarried -= Saloon.getBeerPrice();
	}


	// Enable steering behaviour and disable wandering behaviour
	public void enableEvasion(int vel){
		thiefEvasion.enabled = true;
		thiefWander.enabled = false;
		animator.SetInteger ("speed", vel);
	}
	
	// Disable steering behaviour and enable wandering behaviour
	public void disableEvasion(){
		thiefWander.enabled = true;
		thiefEvasion.enabled = false;
		animator.SetInteger ("speed", 0);
	}

	public void setEvasion(){
		thiefEvasion = GetComponent<SteerForEvasion> ();
		thiefTarget = GameObject.Find("Miner");
		//thiefEvasion.Menace = thiefTarget.transform;
		thiefTargetPos = thiefEvasion.Menace.transform.position;

	}
}

