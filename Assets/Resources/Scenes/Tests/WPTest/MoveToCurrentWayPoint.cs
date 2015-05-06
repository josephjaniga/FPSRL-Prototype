﻿using UnityEngine;
using System.Collections;

public class MoveToCurrentWayPoint : MonoBehaviour {

    Animator anim;
    public Target t;
    Transform currentWP;


	// Use this for initialization
	void Start () {
        t = gameObject.GetComponent<Target>();
	    anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if ( t.waypointCounts > 0 && t.waypointQueue.Peek() != null)
        {
            currentWP = t.waypointQueue.Peek();
        }
        else
        {
            currentWP = null;
        }


        if (currentWP != null)
        {
            // face the current waypoint
            Vector3 normalized = currentWP.transform.position;
            normalized.y = gameObject.transform.position.y;
            gameObject.transform.LookAt(normalized);

            // check if reached
            if (Mathf.Abs(Vector3.Distance(gameObject.transform.position, normalized)) > 0.1f)
            {
                // continue towards it
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
            
        }

	}
}