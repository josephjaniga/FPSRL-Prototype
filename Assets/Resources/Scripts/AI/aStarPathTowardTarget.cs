using UnityEngine;
using System.Collections;

public class aStarPathTowardTarget : MonoBehaviour {

    public Target t;
	public AStarPathfinder asp;
	
	public float speed = 0.1f;
	Animator anim;
	
	// path update speed
	public float pathUpdateCD = 1f;
	public float lastPathUpdate = -1f;
	
	public AStarNode currentWaypoint = null;
	public Vector3 normalizedWaypoint = Vector3.zero;
	
	// Use this for initialization
	void Start () {
        t = gameObject.GetComponent<Target>();
		asp = gameObject.GetComponent<AStarPathfinder>();
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if ( currentWaypoint == null && asp.waypoints.Count > 0 ){
			// set a waypoint if blank
			currentWaypoint = asp.waypoints.Pop();
			normalizedWaypoint = currentWaypoint.pos;
			normalizedWaypoint.y = gameObject.transform.position.y;
		} else if ( Vector3.Distance(gameObject.transform.position, normalizedWaypoint) < .1f && asp.waypoints.Count > 0 ){
			// if reached current waypoint set next
			currentWaypoint = asp.waypoints.Pop();
			normalizedWaypoint = currentWaypoint.pos;
			normalizedWaypoint.y = gameObject.transform.position.y;
		}

		if ( t.target != null && pathUpdateCD + lastPathUpdate <= Time.time ) {
			targetUpdated();
			lastPathUpdate = Time.time;
		}

		if ( gameObject.GetComponent<Target>().target != null && anim != null && !anim.GetBool("Attacking") ){
			// if theres a target and a waypoint
			if ( asp.waypoints.Count > 0 && currentWaypoint != null ){
				Vector3 point = currentWaypoint.pos;
				point.y = gameObject.transform.position.y;
				transform.LookAt(point);
			    anim.SetFloat("Forward", speed);
			}
		} else {
            if (anim != null) {
                anim.SetFloat("Forward", 0.0f);
            }
		}

	}

	public void targetUpdated(){
		//Debug.Log ("targetUpdated");
		asp.destination = asp.nearestNode(t.target.transform.position);
		asp.source = asp.nearestNode(gameObject.transform.position);
		asp.current = asp.source;
	}
	
}
