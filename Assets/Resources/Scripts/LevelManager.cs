using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public int currentLevelNumber = 0;
	public GameObject player;
	public Health playerHealth;

	// the concretes
	public GameObject ui;
	public GameObject mobs;
	public GameObject effects;
	public GameObject stuff;
	public GameObject levelGeometry;
	public GameObject mapper;
    public GameObject theNet;

    // a star pathfinding helpers
    public GameObject aStar;
    public GameObject waypoints;

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

		// get / set player
		player = _.player;
		playerHealth = _.playerHealth;

		// create the UI
		ui = _.ui;

		// set player StartPosition
		player.transform.position = _.playerStartPosition.position;

		mobs = _.mobs;
		stuff = _.stuff;
		effects = _.effects;
		levelGeometry = _.levelGeometry;
		mapper = _.mapper;

        aStar = _.aStar;
        waypoints = _.waypoints;
        theNet = _.theNet;

	}

}
