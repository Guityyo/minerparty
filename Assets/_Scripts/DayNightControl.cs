using UnityEngine;
using System.Collections;

public enum DayTime { morning, day, evening, night };

public class DayNightControl : MonoBehaviour {
	private GameObject mainLight;
	public DayTime currentDayTime;
	public float currentTime;
	public int dayLengthInSec = 24;
	public float maxLightIntensity = 0.7F;
	private float gameStartTime;
	private int morningStartTime, dayStartTime, eveningStartTime, nightStartTime;

	// Use this for initialization
	void Start () {
		mainLight = GameObject.Find ("Main light");

		morningStartTime = 4;  // 4 am on a 24 h day
		dayStartTime = 6;      // 6 am on a 24 h day
		eveningStartTime = 22; // 10 pm on a 24 h day
		nightStartTime = 0;    // midnight on a 24 h day

		gameStartTime = morningStartTime;
		currentDayTime = DayTime.morning;
		mainLight.light.intensity = 0.0f;
	}
	
	// FixedUpdate is called once per physics step, in a predefined interval - important for having a smooth light fadeing
	void FixedUpdate () {
		updateDayTime ();
		updateLightIntensity ();
	}

	void updateDayTime() {
		currentTime = 24 * Mathf.Repeat (Time.realtimeSinceStartup + gameStartTime * dayLengthInSec / 24, dayLengthInSec) / dayLengthInSec;

		int currentTimeInt = Mathf.FloorToInt (currentTime);
		if (currentTimeInt == morningStartTime) setDayTime(DayTime.morning);
		else if (currentTimeInt == dayStartTime) setDayTime(DayTime.day);
		else if (currentTimeInt == eveningStartTime) setDayTime(DayTime.evening);
		else if (currentTimeInt == nightStartTime) setDayTime(DayTime.night);
	}

	void setDayTime(DayTime newDayTime) {
		if (currentDayTime != newDayTime) {
			Debug.Log(newDayTime + " is coming");
			currentDayTime = newDayTime;
		}
	}

	// Stepwidth with which the light needs to be incremented in the morning
	private float lightIncrementStep() {
		float sunriseHours = dayStartTime - morningStartTime;
		float sunriseInSeconds = sunriseHours * dayLengthInSec / 24;
		float fixedFramesForSunrise = sunriseInSeconds / Time.fixedDeltaTime;
		return maxLightIntensity / fixedFramesForSunrise;
	}

	// Stepwidth with which the light needs to be decremented in the evening
	private float lightDecrementStep() {
		float sunsetHours = (nightStartTime < eveningStartTime ? nightStartTime + 24 : nightStartTime) - eveningStartTime;
		float sunsetInSeconds = sunsetHours * dayLengthInSec / 24;
		float fixedFramesForSunset = sunsetInSeconds / Time.fixedDeltaTime;
		return maxLightIntensity / fixedFramesForSunset;
	}

	void updateLightIntensity() {
		switch (currentDayTime) {
			case DayTime.morning:
				mainLight.light.intensity += lightIncrementStep ();
				break;
			case DayTime.day:
				mainLight.light.intensity = maxLightIntensity;
				break;
			case DayTime.evening:
				mainLight.light.intensity -= lightDecrementStep ();
				break;
			case DayTime.night:
				mainLight.light.intensity = 0.0F;
				break;
		}
	}

	public string timeString() {
		int hours = Mathf.FloorToInt (currentTime);
		int minutes = Mathf.FloorToInt ((currentTime - hours) * 60);
		return hours.ToString("00") + ":" + minutes.ToString("00");
	}
}
