using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Target : MonoBehaviour {
	
	public GameObject desiredTarget;
	public GameObject target;

    public Queue<Transform> waypointQueue = new Queue<Transform>();
    public int waypointCounts = 0;

	public AStarNode lastNearest = null;
	public AStarNode thisNearest = null;

	void Update()
	{
        waypointCounts = waypointQueue.Count;

		// if target moves adjust path
		thisNearest = nearestNode(target.gameObject.transform.position);
		if ( lastNearest != thisNearest ){
			Debug.Log("target moved");
			gameObject.SendMessage("RecalculatePath", SendMessageOptions.DontRequireReceiver);
			lastNearest = thisNearest;
		}

	}

    public AStarNode nearestNode(Vector3 pos)
    {
		AStarNodeManager asnm = GameObject.Find ("A*").GetComponent<AStarNodeManager>();
		AStarNode nearest = null;
		
		for ( int i=0; i < asnm.all.Count; i++ ){
			if ( nearest == null ){
				nearest = asnm.all[i];
			} else {
				float distanceToCurrent = Vector3.Distance(pos, asnm.all[i].pos);
				float distanceToNearest = Vector3.Distance(pos, nearest.pos);
				if ( distanceToCurrent < distanceToNearest ){
					nearest = asnm.all[i];
				}
			}
		}
		
		return nearest;
	}
	
}
		

