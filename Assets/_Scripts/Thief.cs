using UnityEngine;
using UnitySteer.Behaviors;
using System.Collections;


[RequireComponent(typeof(SteerToFollow))]
[RequireComponent(typeof(Wander))]
public class Thief : MonoBehaviour {
	Perspective perspective;
	private SteerToFollow thiefSteering;
	private Wander thiefWander;
	private Animator animator;
	public Vector3 thiefCurrPos;
	public Vector3 thiefTargetPos = new Vector3 ();
	public GameObject thiefTarget;
	public int GoldCarried = 0;
	private Miner minerScript;
	public bool moneyStolen = false;

	// To avoid obstacles
	private Vector3 dir;
	public float force = 50.0f;
	public float minimumDistToAvoid = 4.0f;

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

		// AvoidObstacles method 
		dir = (thiefTargetPos - thiefCurrPos);
		dir.Normalize();
		AvoidObstacles (ref dir);
	}
	
	// Method to check if the Thief has arrived to the target
	public bool IsNearTarget(int dist){
		return Vector3.Distance (thiefCurrPos, thiefTargetPos) <= dist;
	}
	
	// To set the target of the steering behaviour
	public void setTarget(string newTarget){
		thiefSteering = GetComponent<SteerToFollow> ();	
		thiefTarget = GameObject.Find(newTarget);
		thiefSteering.Target = thiefTarget.transform;   // assign better the target
		thiefTargetPos = thiefSteering.Target.transform.position;
		
		// for obstacle avoidance
		dir = (thiefTargetPos - thiefCurrPos); // directional vector to target position
		dir.Normalize();
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
	public void enableWandering(int vel){
		thiefWander.enabled = true;
		animator.SetInteger ("speed", vel);
	}
	// Disable wander behaviour and enable Idle animator
	public void disableWandering(){
		thiefWander.enabled = false;
		animator.SetInteger ("speed", 0);
	}
	
	public bool isAt(GameObject location){
		return Vector3.Distance (thiefCurrPos, location.transform.position) <= 3.0;
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

	public void notChased() {
		moneyStolen = false;
	}
	
	public bool hasStolenMoney(){
		return moneyStolen;
	}
	
	public void drinkBeer(){
		GoldCarried -= Saloon.getBeerPrice();
	}



	public void AvoidObstacles(ref Vector3 dir)
	{
		RaycastHit hit;
		
		//Only detect layer 8 (Obstacles)
		int layerMask = 1 << 8;
		
		//Check that the character hit with the obstacles within its minimum distance to avoid
		if (Physics.Raycast(transform.position, transform.forward, out hit, minimumDistToAvoid, layerMask))
		{
			
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

