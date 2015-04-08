using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject player;
	public Health playerHealth;
	public Transform playerStartPosition;

	public GameObject ui;

	// Use this for initialization
	void Start () {
		init ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if ( Input.GetKeyDown (KeyCode.Escape) ){
			Application.Quit ();
		}

	}

	public void init()
	{
		
		// create the UI
		ui = _.ui;


		// get or create start position
		playerStartPosition = _.playerStartPosition;

		// get / set player
		player = _.player;
		playerHealth = _.playerHealth;

		// set player StartPosition
		player.transform.position = playerStartPosition.position;


	}

}
