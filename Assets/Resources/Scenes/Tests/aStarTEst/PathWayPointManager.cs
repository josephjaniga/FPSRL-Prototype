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

        // check if the new path first waypoint is identical to path first waypoint
        Vector3 currentAlpha = t.waypointQueue.Peek().position;
        Vector3 newPathAlpha = asp.waypointQueue.Peek().position;

        // if not clear and update waypoints from path
        if (currentAlpha != newPathAlpha)
        {
            // clear out the waypoints on target
            // Instantiate new waypoints
        }

        // elimiate waypoints if reached?
        // eliminate waypoints from pathfinding if closer than "reached" distance??
        // FIXME: how do we do this ^^^^ ??
	}
}
