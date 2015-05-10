using UnityEngine;
using System.Collections;

public class MoveToCurrentWayPoint : MonoBehaviour {

	public float REACHED_DISTANCE = 1f;

    Animator anim;
    public Target t;
    public Transform currentWP;
    public float movementSpeed = 1.5f;
    public AStarPathfinder asp;

	// Use this for initialization
	void Start () {
        t = gameObject.GetComponent<Target>();
	    anim = gameObject.GetComponent<Animator>();
        asp = gameObject.GetComponent<AStarPathfinder>();
	}
	
	// Update is called once per frame
	void Update () {

        // if we dont have a waypoint and we have a target??

        // check and track curent waypoint
		if ( currentWP == null && asp.waypointQueue.Count > 0 && asp.waypointQueue.Peek() != null)
        {
			currentWP = asp.waypointQueue.Dequeue();
        }

        if (currentWP != null)
        {
            // face the current waypoint
            Vector3 normalized = currentWP.transform.position;
            normalized.y = gameObject.transform.position.y;
            gameObject.transform.LookAt(normalized);

            // check if reached
			if ( Mathf.Abs(Vector3.Distance(gameObject.transform.position, normalized)) > REACHED_DISTANCE )
            {
                // continue towards it
                MoveTowardCurrentWayPoint();
            }
            else
            {
               currentWP = null;
            }

        }
//        else
//        {
//            // if we have a queue of waypoints
//            if ( asp.waypointQueue.Count > 0 )
//            {
//                
//            }
//        }

	}

    public void MoveTowardCurrentWayPoint()
    {
        // TODO: IMPLEMENT THIS BASED ON THE ACTUAL UNIT MOTION
        // anim.SetBool("isMoving", true);
        gameObject.transform.position = Vector3.MoveTowards(
                    gameObject.transform.position,
                    currentWP.position,
                    movementSpeed * Time.deltaTime
                );
    }

	public void WaypointsPopulated(){
		if ( asp.waypointQueue.Count > 0 && asp.waypointQueue.Peek() != null ){
			currentWP = asp.waypointQueue.Dequeue();
		}
	}
}