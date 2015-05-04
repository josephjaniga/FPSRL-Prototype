using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(AStarPathfinder))]
public class AStarPathVisualizer : MonoBehaviour {

	public AStarPathfinder asp;

	public float pathRedrawCD = 0.5f;
	public float lastPathDrawn = -1f;

	// Use this for initialization
	void Start () {
		asp = gameObject.GetComponent<AStarPathfinder>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if ( pathRedrawCD + lastPathDrawn <= Time.time ){
			allNodesGray();
			colorizeWaypoints();
			lastPathDrawn = Time.time;
		}

	}
	
	public void allNodesGray (){
		// make all the nodes color white
		foreach ( Transform child in GameObject.Find ("A*").transform ){
			child.gameObject.GetComponent<Renderer>().material.color = Color.white;
			child.localScale = new Vector3(.1f, .1f, .1f);
		}
	}

	public void colorizeWaypoints(){
		
		Stack<AStarNode> temp = asp.waypoints;
		while ( temp.Count > 0 ){
			AStarNode n = temp.Pop();
			GameObject t = GameObject.Find (n.pos.ToString());
			if (t != null){
				t.GetComponent<Renderer>().material.color = Color.red;
				t.transform.localScale = new Vector3(.25f, .25f, .25f);
			}
		}
		
	}
	
}
