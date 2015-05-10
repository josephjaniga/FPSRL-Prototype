using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Target : MonoBehaviour {

	public GameObject desiredTarget;
	public GameObject target;

    public Queue<Transform> waypointQueue = new Queue<Transform>();
    public int waypointCounts = 0;

	void Update()
	{
        waypointCounts = waypointQueue.Count;
		// raycast to desiredTarget
		// if it can be seen, set it to the target


	}
	
}
