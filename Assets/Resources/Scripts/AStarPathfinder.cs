﻿using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class AStarPathfinder : MonoBehaviour {

	public AStarNodeManager asnm;

	protected AStarNode concreteNode = new AStarNode();

    public AStarNode source = null;
    public AStarNode destination = null;
	public AStarNode current = null;

    public List<AStarNode> open    = new List<AStarNode>();
    public List<AStarNode> closed  = new List<AStarNode>();

	public Stack<AStarNode> waypoints = new Stack<AStarNode>();
	public AStarNode[] waypointDisplay;

	public AStarNode bestChoice = null;

	// path update speed
	public float pathCalculateCD = 0.2f;
	public float lastPathCalculate = -1f;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{

		if ( pathCalculateCD + lastPathCalculate <= Time.time && destination != null )
        {
            allNodesGray();
			crunchPath();
			lastPathCalculate = Time.time;
			destination = null;
        }

		colorizeWaypoints();
		
	}

	public AStarNode nearestNode(Vector3 pos)
	{
		asnm = GameObject.Find ("A*").GetComponent<AStarNodeManager>();
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

	public AStarNode nearestClosed(Vector3 pos)
	{
		AStarNode nearest = null;
		
		for ( int i=0; i < closed.Count; i++ ){
			if ( nearest == null ){
				nearest = closed[i];
			} else {
				float distanceToCurrent = Vector3.Distance(pos, closed[i].pos);
				float distanceToNearest = Vector3.Distance(pos, nearest.pos);
				if ( distanceToCurrent < distanceToNearest ){
					nearest = closed[i];
				}
			}
		}
		
		return nearest;
	}

//	public void AStarInit(){
//		asnm = GameObject.Find ("A*").GetComponent<AStarNodeManager>();
//		source = nearestNode(_.player.transform.position);
//		current = source;
//		destination = asnm.all[Random.Range(0, asnm.all.Count-1)];
//	}

	public List<AStarNode> getAdjacentNodes(AStarNode asn){
		// eight cardinal directions
		AStarNode north      = nearestNode(asn.pos + new Vector3( 0f, 0f,  1f) * concreteNode.unitSize);
		AStarNode northEast  = nearestNode(asn.pos + new Vector3( 1f, 0f,  1f) * concreteNode.unitSize);
		AStarNode east       = nearestNode(asn.pos + new Vector3( 1f, 0f,  0f) * concreteNode.unitSize);
		AStarNode southEast  = nearestNode(asn.pos + new Vector3( 1f, 0f, -1f) * concreteNode.unitSize);
		AStarNode south      = nearestNode(asn.pos + new Vector3( 0f, 0f, -1f) * concreteNode.unitSize);
		AStarNode southWest  = nearestNode(asn.pos + new Vector3(-1f, 0f, -1f) * concreteNode.unitSize);
		AStarNode west       = nearestNode(asn.pos + new Vector3(-1f, 0f,  0f) * concreteNode.unitSize);
		AStarNode northWest  = nearestNode(asn.pos + new Vector3(-1f, 0f,  1f) * concreteNode.unitSize);

		List<AStarNode> temp = new List<AStarNode>();
		if ( !open.Contains(north) )		{ temp.Add(north); }
		if ( !open.Contains(northEast) )	{ temp.Add(northEast); }
		if ( !open.Contains(east) )			{ temp.Add(east); }
		if ( !open.Contains(southEast) )	{ temp.Add(southEast); }
		if ( !open.Contains(south) )		{ temp.Add(south); }
		if ( !open.Contains(southWest) )	{ temp.Add(southWest); }
		if ( !open.Contains(west) )			{ temp.Add(west); }
		if ( !open.Contains(northWest) )	{ temp.Add(northWest); }
		return temp;
	}

    public List<AStarNode> getAdjacentNodesByName(AStarNode asn)
    {
        // eight cardinal directions
        AStarNode north = 		asnm.getNodeByName((asn.pos + new Vector3( 0f, 0f,  1f) * concreteNode.unitSize).ToString());
        AStarNode northEast = 	asnm.getNodeByName((asn.pos + new Vector3( 1f, 0f,  1f) * concreteNode.unitSize).ToString());
        AStarNode east = 		asnm.getNodeByName((asn.pos + new Vector3( 1f, 0f,  0f) * concreteNode.unitSize).ToString());
        AStarNode southEast = 	asnm.getNodeByName((asn.pos + new Vector3( 1f, 0f, -1f) * concreteNode.unitSize).ToString());
        AStarNode south = 		asnm.getNodeByName((asn.pos + new Vector3( 0f, 0f, -1f) * concreteNode.unitSize).ToString());
        AStarNode southWest = 	asnm.getNodeByName((asn.pos + new Vector3(-1f, 0f, -1f) * concreteNode.unitSize).ToString());
        AStarNode west = 		asnm.getNodeByName((asn.pos + new Vector3(-1f, 0f,  0f) * concreteNode.unitSize).ToString());
        AStarNode northWest = 	asnm.getNodeByName((asn.pos + new Vector3(-1f, 0f,  1f) * concreteNode.unitSize).ToString());

        List<AStarNode> temp = new List<AStarNode>();
		if ( north != null ){ temp.Add (north); }
		if ( northEast != null ){ temp.Add (northEast); }
		if ( east != null ){ temp.Add (east); }
		if ( southEast != null ){ temp.Add (southEast); }
		if ( south != null ){ temp.Add (south); }
		if ( southWest != null ){ temp.Add (southWest); }
		if ( west != null ){ temp.Add (west); }
		if ( northWest != null ){ temp.Add (northWest); }
        return temp;
    }

	public List<AStarNode> getAdjacentNodesByRelationship(AStarNode asn)
	{
		//Debug.Log(asn);
		
		List<AStarNode> temp = new List<AStarNode>();
		if ( asn.north != null ){ temp.Add (asn.north); }
		if ( asn.northEast != null ){ temp.Add (asn.northEast); }
		if ( asn.east != null ){ temp.Add (asn.east); }
		if ( asn.southEast != null ){ temp.Add (asn.southEast); }
		if ( asn.south != null ){ temp.Add (asn.south); }
		if ( asn.southWest != null ){ temp.Add (asn.southWest); }
		if ( asn.west != null ){ temp.Add (asn.west); }
		if ( asn.northWest != null ){ temp.Add (asn.northWest); }
		return temp;
	}
	
//	public void displayPath(){
//		
//		if ( GameObject.Find("Debugging") != null ){
//
//            foreach (Transform child in GameObject.Find("Debugging").transform)
//            {
//                Destroy(child.gameObject);
//            }
//
//			GameObject prefab = Resources.Load("Prefabs/aStarNode") as GameObject;
//			
//			GameObject src = Instantiate( prefab, source.pos, Quaternion.identity ) as GameObject;
//			src.GetComponent<Renderer>().material.color = Color.green;
//			src.transform.localScale = new Vector3(1f, 0.05f, 1f);
//			src.name = "Source";
//			src.transform.SetParent(GameObject.Find ("Debugging").transform);
//			
//			GameObject dest = Instantiate( prefab, destination.pos, Quaternion.identity ) as GameObject;
//			dest.GetComponent<Renderer>().material.color = Color.red;
//			dest.transform.localScale = new Vector3(1f, 0.05f, 1f);
//			dest.name = "Destination";
//			dest.transform.SetParent(GameObject.Find ("Debugging").transform);
//			
//			// debug open
//            for ( int i=0; i<open.Count; i++ ){
//				// highlight the open list
//				GameObject tempOpen = Instantiate( prefab, open[i].pos, Quaternion.identity ) as GameObject;
//				tempOpen.GetComponent<Renderer>().material.color = Color.blue;
//				tempOpen.transform.localScale = new Vector3(1f, 0.040f, 1f);
//				tempOpen.name = "Open";
//				tempOpen.transform.SetParent(GameObject.Find ("Debugging").transform);
//			}
//            
//			// debug closed
//			for ( int i=0; i<closed.Count; i++ ){
//				// highlight the closed list
//				GameObject tempClosed = Instantiate( prefab, closed[i].pos, Quaternion.identity ) as GameObject;
//				tempClosed.GetComponent<Renderer>().material.color = Color.cyan;
//				tempClosed.transform.localScale = new Vector3(1f, 0.045f, 1f);
//				tempClosed.name = "Closed";
//				tempClosed.transform.SetParent(GameObject.Find ("Debugging").transform);
//			}
//
//        }
//
//    }


	public void calculatePathTo(AStarNode destination){

		// clear duplicates
		open.Distinct().ToList();
		closed.Distinct().ToList();

		// remove all matching items in the open list from the closed list
		for ( int i=0; i<open.Count; i++ ){
			if ( closed.Contains(open[i]) ){
				open.RemoveAt(i);
				i--;
			}
		}

		bestChoice = null;

		// assign values to the opens
		for ( int i=0; i<open.Count; i++ ){
			AStarNode temp = open[i];
			calculateCosts(current, ref temp, destination);
			if ( bestChoice == null ){
				bestChoice = temp;
			} else {
				if ( bestChoice.F > temp.F ){
					bestChoice = temp;
				}
			}
		}
		
		if ( bestChoice != null ){
			
			// make the switch
			open.Remove(bestChoice);
			closed.Add(bestChoice);
			closed.Distinct().ToList();
			current = bestChoice;

			if ( current != destination && open.Count != 0 ){
				
				// open the adjacent nodes
				foreach ( AStarNode n in getAdjacentNodes(current) ){
					// walkable and not on the closed list
					if ( n.walkable && !closed.Contains(n) ){
						AStarNode temp = n;
						if (!open.Contains(temp)) {// not on open list?
							open.Add(temp);
							temp.parent = current;
							calculateCosts(current, ref temp, destination);
						} else if (temp.G < current.G) {
							temp.parent = current;
							calculateCosts(current, ref temp, destination);
						}
					}
				}
				
				// keep calculating
				calculatePathTo(destination);
				
			} else {
				
				// set the waypoints
				waypoints.Clear();
				AStarNode n = destination;
				while ( n != null ){
					waypoints.Push(n);
					n = n.parent;
				}
				waypointDisplay = waypoints.ToArray();
				
			}

		} else {
			
			// set the waypoints
			waypoints.Clear();
			AStarNode n = destination;
			while ( n != null ){
				waypoints.Push(n);
				n = n.parent;
			}
			waypointDisplay = waypoints.ToArray();
			
		}
		
	

	}

	public void clearLists(){
		open    = new List<AStarNode>();
		closed  = new List<AStarNode>();
	}

	public void allNodesGray (){
		// make all the nodes color white
		foreach ( Transform child in GameObject.Find ("A*").transform ){
			child.gameObject.GetComponent<Renderer>().material.color = Color.white;
			child.localScale = new Vector3(.1f, .1f, .1f);
		}
	}

	public void colorizeClosedPath(){

//		foreach (AStarNode n in closed){
//			GameObject t = GameObject.Find (n.pos.ToString());
//			t.GetComponent<Renderer>().material.color = Color.green;
//			t.transform.localScale = new Vector3(.25f, .25f, .25f);
//		}

		AStarNode n = destination;
		while ( n != null ){
			GameObject t = GameObject.Find (n.pos.ToString());
			t.GetComponent<Renderer>().material.color = Color.red;
			t.transform.localScale = new Vector3(.25f, .25f, .25f);
			n = n.parent;
		}

	}
	
	public void colorizeWaypoints(){

		Stack<AStarNode> temp = waypoints;
		while ( temp.Count > 0 ){
			AStarNode n = temp.Pop();
			GameObject t = GameObject.Find (n.pos.ToString());
			if (t != null){
				t.GetComponent<Renderer>().material.color = Color.red;
				t.transform.localScale = new Vector3(.25f, .25f, .25f);
			}
		}

	}

	public void calculateCosts(AStarNode current, ref AStarNode testNode, AStarNode destination){

		// FIXME: redo G cost - MovementCost
		testNode.G = calculateMovementCost(testNode);

		// heuristic cost
		int xUnitsAway = Mathf.RoundToInt(Mathf.Abs( (testNode.pos.x - destination.pos.x) / testNode.unitSize ));
		int zUnitsAway = Mathf.RoundToInt(Mathf.Abs( (testNode.pos.z - destination.pos.z) / testNode.unitSize ));
		testNode.H = xUnitsAway * testNode.HV_cost + zUnitsAway * testNode.HV_cost;
		testNode.F = testNode.G + testNode.H;

	}

	public int calculateMovementCost(AStarNode n){
		if ( n == source || n.parent == null ){
			return 0;
		} else {
			// movement cost
			if ( n.pos.x == n.parent.pos.x || n.pos.z == n.parent.pos.z ){
				// horizontal or vertical
				return n.HV_cost + calculateMovementCost(n.parent);
			} else {
				// diagonal
				return n.D_cost + calculateMovementCost(n.parent);
			}
		}
	}
	


	/**
	 *  NEW PATHING
     */

	
	public void crunchPath(){
		
		if ( !closed.Contains(destination) && open.Count > 0 ){
			// get lowest F COST on open
			setBestChoice();
			current = bestChoice;
			
			// move bestChoice to closed list
			open.Remove(bestChoice);
			closed.Add(bestChoice);
			
			// for each adjacent 8 to current
			foreach ( AStarNode n in getAdjacentNodes(current) ){
				// ignore if not walkabled or already closed
				if ( n.walkable && !closed.Contains(n) ){
					// if not open
					AStarNode temp = n;
					if ( !open.Contains(temp) ){
						// add to open, set this.parent = current, recalculate costs
						temp.parent = current;
						calculateCosts(current, ref temp, destination);
						open.Add(temp);
					} else if (temp.G < current.G) {
						// else if open
						// if cost is better than current G
						// set this.parent = current, recalculate costs
						temp.parent = current;
						calculateCosts(current, ref temp, destination);
					}
				}
			}

			// another pass
			crunchPath();

		} else {

			//DONE
			Stack<AStarNode> tempWaypoints = new Stack<AStarNode>();

			AStarNode n = destination;
			while ( n != null ){
				tempWaypoints.Push(n);
				n = n.parent;
			}

			if ( waypoints != tempWaypoints ){
				waypoints.Clear();
				waypoints = tempWaypoints;
				waypointDisplay = waypoints.ToArray();
				gameObject.SendMessage("PathCalculated", SendMessageOptions.DontRequireReceiver);
			}


		}

	}

	public void setBestChoice(){

		bestChoice = null;
		
		// assign values to the opens
		for ( int i=0; i<open.Count; i++ ){
			AStarNode temp = open[i];
			calculateCosts(current, ref temp, destination);
			if ( bestChoice == null ){
				bestChoice = temp;
			} else {
				if ( bestChoice.F > temp.F ){
					bestChoice = temp;
				}
			}
		}

	}

}
