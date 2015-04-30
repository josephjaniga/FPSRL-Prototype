using UnityEngine;
using System.Collections;

public class MoveToClickLocation : MonoBehaviour {

	public Camera cam;

	public GameObject pather;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if ( Input.GetMouseButtonDown(0) ){

			Ray ray;
			ray = cam.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit;
			// Casts the ray and get the first game object hit

			if ( Physics.Raycast(ray, out hit) ){
				gameObject.transform.position = hit.point;
			}

			if ( pather != null ){
				pather.SendMessage("targetUpdated", SendMessageOptions.DontRequireReceiver);
			}

		}

	}
}
