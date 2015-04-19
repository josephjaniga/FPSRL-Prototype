using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public Camera FPSCamera;
	public Camera WeaponClippingCamera;
	public Camera PistolModelCamera;

	public GameObject WeaponUpgradesPanel;
	public bool WUP_panel_shown = false;

	// Use this for initialization
	void Start () {
	
		WeaponUpgradesPanel = GameObject.Find("WeaponUpgradesPanel");
		WeaponUpgradesPanel.SetActive(false);

		FPSCamera = 			_.player.transform.FindChild ("FirstPersonCharacter").gameObject.GetComponent<Camera>();
		WeaponClippingCamera =  GameObject.Find("WeaponCamera").GetComponent<Camera>();
		PistolModelCamera = 	GameObject.Find("PistolModelCamera").GetComponent<Camera>();

		PistolModelCamera.enabled = false;

		Cursor.lockState = CursorLockMode.Confined;
	}
	
	// Update is called once per frame
	void Update () {

		if ( Input.GetKeyDown(KeyCode.I) ){
			WUP_panel_shown = !WUP_panel_shown;
		}

		if ( Input.GetKeyDown(KeyCode.Escape) ){
			WUP_panel_shown = false;
		}

		if ( !WUP_panel_shown ){
			Cursor.visible = false;
			WeaponUpgradesPanel.SetActive(false);
			PistolModelCamera.enabled = false;
		} else {
			Cursor.visible = true;
			WeaponUpgradesPanel.SetActive(true);
			PistolModelCamera.enabled = true;
		}

	}
}
