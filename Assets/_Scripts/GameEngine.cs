using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {

	// Init vars before game starts
	void Awake() {
	Debug.Log ("Hello, I am awake (after snoozing my alarm too often!)");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject go;
		Miner script;

		go = GameObject.Find ("Miner");
		if (go != null) {
			script = go.GetComponent<Miner>();
			if (script.MoneyInBank > 10000) {
				Debug.Log("Yuppie weeeeee! I am riiiiich :D");
				//Destroy(go);
				Application.Quit();
			}
		}
	}
}
