using UnityEngine;
using System.Collections;

public class aStarPathTowardTarget : MonoBehaviour {

    public Target t;
	public AStarPathfinder asp;

	// Use this for initialization
	void Start () {
        t = gameObject.GetComponent<Target>();
		asp = gameObject.GetComponent<AStarPathfinder>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void targetUpdated(){

		//Debug.Log ("event received");
		asp.destination = asp.nearestNode(t.target.transform.position);
		asp.source = asp.nearestNode(gameObject.transform.position);
		asp.current = asp.source;

	}


}
