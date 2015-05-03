using UnityEngine;
using System.Collections;

public class aStarPathTowardTarget : MonoBehaviour {

    public Target t;
	public AStarPathfinder asp;
	
	public float speed = 0.1f;
	Animator anim;
	
	// Use this for initialization
	void Start () {
        t = gameObject.GetComponent<Target>();
		asp = gameObject.GetComponent<AStarPathfinder>();
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if ( t.target != null ) {
			targetUpdated();
		}

		if ( gameObject.GetComponent<Target>().target != null && anim != null && !anim.GetBool("Attacking") ){
			// if theres a target and a waypoint
			if ( asp.waypoint != null ){
				Vector3 point = asp.waypoint.pos;
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

        if (asp.destination != null)
        {
            asp.destinationPosition = asp.destination.pos;
        }
        else
        {
            asp.destinationPosition = Vector3.zero;
        }

        if (asp.source != null)
        {
            asp.sourcePosition = asp.source.pos;
        }
        else
        {
            asp.sourcePosition = Vector3.zero;
        }

        if (asp.current != null)
        {
            asp.currentPosition = asp.current.pos;
        }
        else
        {
            asp.currentPosition = Vector3.zero;
        }

        Debug.Break();
	}
	
}
