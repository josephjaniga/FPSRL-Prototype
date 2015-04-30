using UnityEngine;
using System.Collections;

public class aStarPathTowardTarget : MonoBehaviour {

    public Target t;

	// Use this for initialization
	void Start () {
        t = gameObject.GetComponent<Target>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
