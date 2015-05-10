using UnityEngine;
using System.Collections;

public class PathWayPointManager : MonoBehaviour {

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
}
