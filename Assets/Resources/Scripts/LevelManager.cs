using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject player;
	public Health playerHealth;

	// Use this for initialization
	void Start () {
		init ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void init(){
	
		player = _.player;
		playerHealth = _.playerHealth;

	}

}
