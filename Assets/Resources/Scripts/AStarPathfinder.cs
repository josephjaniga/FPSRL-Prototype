using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarPathfinder : MonoBehaviour {

    AStarNode source = null;
    AStarNode destination = null;

    List<AStarNode> all     = new List<AStarNode>();
    List<AStarNode> open    = new List<AStarNode>();
    List<AStarNode> closed  = new List<AStarNode>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
