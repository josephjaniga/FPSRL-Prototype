using UnityEngine;
using System.Collections;

public class RightClickAddWayPoint : MonoBehaviour {

    public Target t;
    public Camera cam;
    public GameObject wayPointPrefab;

	// Use this for initialization
	void Start () {
        t = gameObject.GetComponent<Target>();
        cam = GameObject.Find("Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(1))
        {
            // raycast for a valid path
            Ray ray;
            ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            // Casts the ray and get the first game object hit

            if (Physics.Raycast(ray, out hit))
            {
                // add a waypoint
                GameObject wp = Instantiate(wayPointPrefab, hit.point, Quaternion.identity) as GameObject;
                t.waypointQueue.Enqueue(wp.transform);
            }
        }


	}
}
