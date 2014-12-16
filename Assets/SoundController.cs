using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {
	public GameObject daylightObject;
	public AudioClip rooster;
	public AudioSource chasingMusic;
	public AudioSource nightMusic;
	public AudioSource dayMusic;
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
		updateMusic();
	}

	private void updateMorningSound() {
		float currentLightIntensity = daylightObject.light.intensity;
		if ((lastLightIntensity < morningBrightness) && (currentLightIntensity >= morningBrightness)) {
			audio.PlayOneShot(rooster);
		}
		lastLightIntensity = currentLightIntensity;
	}

	private void updateMusic() {
		if (minerScript.IsDarkOutside ()) {
			if (dayMusic.isPlaying) dayMusic.Stop();
			if (chasingMusic.isPlaying) chasingMusic.Stop();
			if (! nightMusic.isPlaying) nightMusic.Play();
		} else {
			if (minerScript.IsChasing ()) {
				if (nightMusic.isPlaying) nightMusic.Stop();
				if (dayMusic.isPlaying) dayMusic.Stop();
				if (! chasingMusic.isPlaying) chasingMusic.Play();
			} else {
				if (nightMusic.isPlaying) nightMusic.Stop();
				if (chasingMusic.isPlaying) chasingMusic.Stop();
				if (! dayMusic.isPlaying) dayMusic.Play();
			}
		}
	}
}
