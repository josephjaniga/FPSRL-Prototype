using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public int currentLevelNumber = 0;
	public GameObject player;
	public Health playerHealth;
	public Transform playerStartPosition;

	// the concretes
	public GameObject ui;
	public GameObject mobs;
	public GameObject effects;
	public GameObject stuff;
	public GameObject levelGeometry;
	public GameObject mapper;

	// Use this for initialization
	void Start () {

        if (currentLevelNumber != null)
        {
            currentLevelNumber++;
        }
        else
        {
            currentLevelNumber = 0;
        }

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

		mobs = _.mobs;
		stuff = _.stuff;
		effects = _.effects;
		levelGeometry = _.levelGeometry;
		mapper = _.mapper;


	}

}
