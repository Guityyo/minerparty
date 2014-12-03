using UnityEngine;
using System.Collections;

public class HudController : MonoBehaviour {
	public GameObject MinerGameObj, MainLight, HUDStatusText, HUDMinerText;
	private Miner miner;
	private DayNightControl dayNightControl;

	// Use this for initialization
	void Start () {
		miner = MinerGameObj.GetComponent<Miner>();
		dayNightControl = MainLight.GetComponent<DayNightControl>();
	}

	private string timeString() {
		return "Time:\t" + dayNightControl.timeString();
	}
	
	private string goldString() {
		return "Gold:\t" + miner.GoldCarried.ToString();
	}
	
	private string bankString() {
		return "Bank:\t" + miner.MoneyInBank.ToString();
	}
	
	private string thirstString() {
		return "Thirst:\t" + miner.Thirst.ToString();
	}
	
	private string peeString() {
		return "Pee:\t" + miner.BathroomNeed.ToString();
	}
	
	private string fatigueString() {
		return "Fatigue:\t" + miner.Fatigue.ToString();
	}

	private void updateStatusText() {
		HUDStatusText.guiText.text = 	timeString() + "\n" +
										goldString() + "\n" +
										bankString() + "\n" +
										thirstString() + "\n" +
										peeString() + "\n" +
										fatigueString();
	}

	public void setMinerText(string text) {
		HUDMinerText.guiText.text = text;
	}
	
	// Update is called once per frame
	void Update () {
		updateStatusText();
	}
}
