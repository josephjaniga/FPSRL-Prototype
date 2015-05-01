using UnityEngine;
using System.Collections;

public class TargetPlayerWithinRange : MonoBehaviour {

	GameObject player;
	public float range = 15f;

	// Use this for initialization
	void Start () {
	
		player = _.player;

	}
	
	// Update is called once per frame
	void Update () {
	
		float distToTarget = Vector3.Distance(transform.position, player.transform.position);

		if ( distToTarget <= range ){

			//Vector3 point = player.transform.position;
			//point.y = 0f;
			//transform.LookAt(point);
			gameObject.GetComponent<Target>().target = player;

		} else {

			gameObject.GetComponent<Target>().target = null;

		}

	}
}
