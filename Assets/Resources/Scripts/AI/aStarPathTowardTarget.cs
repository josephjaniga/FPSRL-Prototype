using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class aStarPathTowardTarget : MonoBehaviour {

    public Target t;
	public AStarPathfinder asp;
	
	public float speed = 0.1f;
	Animator anim;
	
	// path update speed
	public float pathUpdateCD = 0.3f;
	public float lastPathUpdate = -1f;
	
	public AStarNode currentWaypoint = null;
	public Vector3 normalizedWaypoint;

	public bool reachedWaypoint = false;

	public float D = 0f;

	public GameObject lastTarget = null;

	public List<AStarNode> completeWaypoints = new List<AStarNode>();

	// Use this for initialization
	void Start () {
        t = gameObject.GetComponent<Target>();
		asp = gameObject.GetComponent<AStarPathfinder>();
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		// update target position
		if ( t.target != null ){
			asp.destination = asp.nearestNode(t.target.transform.position);
		}
		// update my position
		asp.source = asp.nearestNode(gameObject.transform.position);

		// if we have a waypoint
		if ( currentWaypoint != null ){

			// account for different heights of nodes and dont factor that into distance calculations
			normalizedWaypoint = currentWaypoint.pos;
			normalizedWaypoint.y = gameObject.transform.position.y;
			
			// determine the distance to the normalized waypoint
			D = Vector3.Distance(gameObject.transform.position, normalizedWaypoint);
			
			// if the target has changed update it
//			if ( lastTarget != t.target ){
//				lastTarget = t.target;
//				targetUpdated();
//			}

			// keep moving toward the waypoint
			if ( !reachedWaypoint ){
				
				if ( anim != null && !anim.GetBool("Attacking") ){
					// if theres a target and a waypoint
					Vector3 point = normalizedWaypoint;
					transform.LookAt(point);
					anim.SetFloat("Forward", speed);
				} else {
					if (anim != null) {
						anim.SetFloat("Forward", 0.0f);
					}
				}
//
//				if ( anim == null ){
//					gameObject.transform.Translate( (normalizedWaypoint - gameObject.transform.position) * Time.deltaTime/4f );
//				}
			
			} else {
				//request a new waypoint
				while ( asp.waypoints.Count > 0 && completeWaypoints.Contains(currentWaypoint) ){
					currentWaypoint = asp.waypoints.Pop ();
				}
			}

		} else {
			// stop moving

		}

		// has reached waypoint
		if ( D <= 0.1f ){
			completeWaypoints.Add(currentWaypoint);
			reachedWaypoint = true;
		}

		
	}
	
	
	// Should be called after target has changed
	public void targetUpdated(){

		if ( t.target != null ){
			//Debug.Log ("targetUpdated");
			asp.destination = asp.nearestNode(t.target.transform.position);
			asp.source = asp.nearestNode(gameObject.transform.position);
			
			if ( asp.current == null || asp.current != asp.source ){
				asp.current = asp.source;
			}
			
			asp.clearLists();
			asp.open.Add(asp.source);
		}

	}
	
	/**
	 * called back after last iteration of recursive AStarPathfinder::crunchPath()
	 */
	public void PathCalculated(){
//		if ( asp.waypoints.Count > 1 ){
//			// set a waypoint if blank and available
//			currentWaypoint = asp.waypoints.Pop();
//			normalizedWaypoint = currentWaypoint.pos;
//			normalizedWaypoint.y = gameObject.transform.position.y;
//		}
	}
	
	void OnDrawGizmos () {
		if ( currentWaypoint != null ){
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere (currentWaypoint.pos, .3f);
		}
	}
}
