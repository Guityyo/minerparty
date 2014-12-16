using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject minerCameraObj;
	public GameObject thiefCameraObj;
	public GameObject overviewCameraObj;
	private int activeCam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			toggleCamera();
		}
	}

	public void toggleCamera() {
		if (minerCameraObj.activeSelf) {
			minerCameraObj.SetActive(false);
			thiefCameraObj.SetActive(true);
		} else if (thiefCameraObj.activeSelf) {
			thiefCameraObj.SetActive(false);
			overviewCameraObj.SetActive(true);
		}  else if (overviewCameraObj.activeSelf) {
			overviewCameraObj.SetActive(false);
			minerCameraObj.SetActive(true);
		}
	}
}
