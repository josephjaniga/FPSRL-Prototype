using UnityEngine;
using System.Collections;

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

	public float D = 0f;

	// Use this for initialization
	void Start () {
        t = gameObject.GetComponent<Target>();
		asp = gameObject.GetComponent<AStarPathfinder>();
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		D = Vector3.Distance(gameObject.transform.position, normalizedWaypoint);

		if ( D < 1f && asp.waypoints.Count > 0 ){
			// if reached current waypoint set next
			currentWaypoint = asp.waypoints.Pop();
			normalizedWaypoint = currentWaypoint.pos;
			normalizedWaypoint.y = gameObject.transform.position.y;
		}

//		if ( currentWaypoint == null ){
//			if ( asp.waypoints.Count > 0 ){
//				// set a waypoint if blank and available
//				currentWaypoint = asp.waypoints.Pop();
//				normalizedWaypoint = currentWaypoint.pos;
//				normalizedWaypoint.y = gameObject.transform.position.y;
//			}
//		}
//
//		if ( currentWaypoint != null ){
//			normalizedWaypoint = currentWaypoint.pos;
//			normalizedWaypoint.y = gameObject.transform.position.y;
//			if ( Vector3.Distance(gameObject.transform.position, normalizedWaypoint) < 1f && asp.waypoints.Count > 0 ){
//				// if reached current waypoint set next
//				currentWaypoint = asp.waypoints.Pop();
//				normalizedWaypoint = currentWaypoint.pos;
//				normalizedWaypoint.y = gameObject.transform.position.y;
//			}
//		}


		if ( t.target != null && pathUpdateCD + lastPathUpdate <= Time.time ) {
			targetUpdated();
			lastPathUpdate = Time.time;

			if ( gameObject.GetComponent<Target>().target != null && anim != null && !anim.GetBool("Attacking") ){
				// if theres a target and a waypoint
				Vector3 point = normalizedWaypoint;
				transform.LookAt(point);
				anim.SetFloat("Forward", speed);
			} else {
				if (anim != null) {
					anim.SetFloat("Forward", 0.0f);
				}
			}

		}

	}

	public void targetUpdated(){
		//Debug.Log ("targetUpdated");
		asp.destination = asp.nearestNode(t.target.transform.position);
		asp.source = asp.nearestNode(gameObject.transform.position);

		if ( asp.current == null ){
			asp.current = asp.source;
		}

		asp.clearLists();
		asp.open.Add(asp.source);
	}

	public void PathCalculated(){
		if ( asp.waypoints.Count > 0 ){
			// set a waypoint if blank and available
			currentWaypoint = asp.waypoints.Pop();
			normalizedWaypoint = currentWaypoint.pos;
			normalizedWaypoint.y = gameObject.transform.position.y;
		}
	}
	
	void OnDrawGizmos () {
		if ( currentWaypoint != null ){
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere (currentWaypoint.pos, .3f);
		}
	}
}
