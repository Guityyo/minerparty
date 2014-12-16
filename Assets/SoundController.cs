using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {
	public GameObject daylightObject;
	public AudioClip rooster;
	public AudioSource chasingMusic;
	public AudioSource idleMusic;
	public float morningBrightness;
	public Miner minerScript;
	private float lastLightIntensity = 1;


	// Use this for initialization
	void Start () {
		daylightObject = GameObject.Find("Main light");
		minerScript = GameObject.Find("Miner").GetComponent<Miner>();
	}
	
	// Update is called once per frame
	void Update () {
		updateMorningSound();
		updateChasingMusic();
	}

	private void updateMorningSound() {
		float currentLightIntensity = daylightObject.light.intensity;
		if ((lastLightIntensity < morningBrightness) && (currentLightIntensity >= morningBrightness)) {
			audio.PlayOneShot(rooster);
		}
		lastLightIntensity = currentLightIntensity;
	}

	private void updateChasingMusic() {
		if (minerScript.IsChasing ()) {
			if (! chasingMusic.isPlaying) chasingMusic.Play ();
		} else {
			if (chasingMusic.isPlaying) chasingMusic.Stop ();
		}
	}
}
