using UnityEngine;
using UnitySteer.Behaviors;
using System.Collections;

public class Thief : MonoBehaviour {
	Perspective perspective;
	private SteerToFollow thiefSteering;
	private Animator animator;
	public Vector3 thiefCurrPos;
	public Vector3 thiefTargetPos = new Vector3 ();
	public GameObject thiefTarget;
	public int GoldCarried = 0;

	private Vector3 dir;
	//TODO add to avoid obstacles ??

	// Use this for initialization
	void Start () {
		perspective = gameObject.GetComponent (Perspective);
		thiefSteering = GetComponent<SteerToFollow> ();
		animator = GetComponent<Animator> ();

		thiefCurrPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject mine = GameObject.Find("Mine");
		if (perspective.IsWithinViewfield (mine))
						Debug.Log ("jippie!");
	}

	// Method to check if the Thief has arrived to the target
	public bool IsNearTarget(){
		return Vector3.Distance (thiefCurrPos, thiefTargetPos) <= 1.0;
	}

	// To set the target of the steering behaviour
	public void setTarget(string newTarget){
		if(thiefTarget!=newTarget) {
		thiefSteering = GetComponent<SteerToFollow> ();
		animator = GetComponent<Animator> ();
		
		thiefTarget = GameObject.Find(newTarget);
		thiefSteering.Target = thiefTarget.transform;
		
		thiefTargetPos = thiefSteering.Target.transform.position;
		// for obstacle avoidance
		dir = (thiefTargetPos - thiefCurrPos); // directional vector to target position
		dir.Normalize();
		}
	}

	// Enable steering behaviour
	public void enableSteering(int vel){
		thiefSteering.enabled = true;
		animator.SetInteger ("speed", 200);
	}
	
	// Disable steering behaviour
	public void disableSteering(){
		thiefSteering.enabled = false;
		animator.SetInteger ("speed", 0);
	}

	public bool isAt(GameObject location){
		return Vector3.Distance (thiefCurrPos, location.transform.position) <= 1.0;
	}

	// To check if steering is enabled
	public bool isSteeringEnabled(){
		return thiefSteering.enabled;
	}
}

