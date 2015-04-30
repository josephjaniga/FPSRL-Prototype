using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class AStarPathfinder : MonoBehaviour {

	public AStarNodeManager asnm;

	public AStarNode concreteNode = new AStarNode();

    public AStarNode source = null;
    public AStarNode destination = null;
	public AStarNode current = null;

    public List<AStarNode> open    = new List<AStarNode>();
    public List<AStarNode> closed  = new List<AStarNode>();

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
        /*
        Vector3 fwd = _.playerBulletSpawn.TransformDirection(Vector3.forward);
		RaycastHit hit;
        if (Physics.Raycast(_.playerBulletSpawn.position, fwd, out hit))
        {
            destination = nearestNode(hit.point);
            clearLists();
        }
        */

        if (destination != null)
        {
			clearLists();
            calculatePathTo(destination);
            //displayPath();

			allNodesGray();
			colorizeClosedPath();
			destination = null;
        }
		
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

	public void AStarInit(){
		asnm = GameObject.Find ("A*").GetComponent<AStarNodeManager>();
		source = nearestNode(_.player.transform.position);
		current = source;
		destination = asnm.all[Random.Range(0, asnm.all.Count-1)];
	}

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

	public void displayPath(){

		if ( GameObject.Find("Debugging") != null ){

            foreach (Transform child in GameObject.Find("Debugging").transform)
            {
                Destroy(child.gameObject);
            }

			GameObject prefab = Resources.Load("Prefabs/aStarNode") as GameObject;
			
			GameObject src = Instantiate( prefab, source.pos, Quaternion.identity ) as GameObject;
			src.GetComponent<Renderer>().material.color = Color.green;
			src.transform.localScale = new Vector3(1f, 0.05f, 1f);
			src.name = "Source";
			src.transform.SetParent(GameObject.Find ("Debugging").transform);
			
			GameObject dest = Instantiate( prefab, destination.pos, Quaternion.identity ) as GameObject;
			dest.GetComponent<Renderer>().material.color = Color.red;
			dest.transform.localScale = new Vector3(1f, 0.05f, 1f);
			dest.name = "Destination";
			dest.transform.SetParent(GameObject.Find ("Debugging").transform);
			
			// assign values to the opens
			
            for ( int i=0; i<open.Count; i++ ){
				// highlight the open list
				GameObject tempOpen = Instantiate( prefab, open[i].pos, Quaternion.identity ) as GameObject;
				tempOpen.GetComponent<Renderer>().material.color = Color.blue;
				tempOpen.transform.localScale = new Vector3(1f, 0.040f, 1f);
				tempOpen.name = "Open";
				tempOpen.transform.SetParent(GameObject.Find ("Debugging").transform);
			}
            
			
			// assign values to the opens
			for ( int i=0; i<closed.Count; i++ ){
				// highlight the closed list
				GameObject tempClosed = Instantiate( prefab, closed[i].pos, Quaternion.identity ) as GameObject;
				tempClosed.GetComponent<Renderer>().material.color = Color.cyan;
				tempClosed.transform.localScale = new Vector3(1f, 0.045f, 1f);
				tempClosed.name = "Closed";
				tempClosed.transform.SetParent(GameObject.Find ("Debugging").transform);
			}

        }

    }

	public void calculatePathTo(AStarNode destination){

		// open the start position
		open.Add(current);
		// open the adjacent nodes
		open.AddRange(getAdjacentNodes(current));
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

		AStarNode bestChoice = null;

		// assign values to the opens
		for ( int i=0; i<open.Count; i++ ){

			// movement cost
			if ( open[i].pos.x == current.pos.x || open[i].pos.z == current.pos.z ){
				// horizontal or vertical
				open[i].G = open[i].HV_cost;
			} else {
				// diagonal
				open[i].G = open[i].D_cost;
			}

			// heuristic cost
			int xUnitsAway = Mathf.RoundToInt(Mathf.Abs( (open[i].pos.x - destination.pos.x) / open[i].unitSize ));
			int zUnitsAway = Mathf.RoundToInt(Mathf.Abs( (open[i].pos.z - destination.pos.z) / open[i].unitSize ));
			open[i].H = xUnitsAway * open[i].HV_cost + zUnitsAway * open[i].HV_cost;
			open[i].F = open[i].G + open[i].H;

			if ( bestChoice == null ){
				bestChoice = open[i];
			} else {
				if ( bestChoice.F > open[i].F ){
					bestChoice = open[i];
				}
			}

		}

		// make the switch
		open.Remove(bestChoice);
		closed.Add(bestChoice);
		closed.Distinct().ToList();
		current = bestChoice;

		if ( closed.Count > 1 ){
			closed[closed.Count-1].parent = closed[closed.Count-2];
		}

		if ( current != destination && open.Count != 0 ){
			calculatePathTo(destination);
		}

	}

	public void clearLists(){
		open    = new List<AStarNode>();
		closed  = new List<AStarNode>();
	}

	public void allNodesGray (){
		// make all the nodes color white
		foreach ( Transform child in GameObject.Find ("A*").transform ){
			child.gameObject.GetComponent<Renderer>().material.color = Color.gray;
			child.localScale = new Vector3(.1f, .1f, .1f);
		}
	}

	public void colorizeClosedPath(){
		foreach (AStarNode n in closed){
			GameObject t = GameObject.Find (n.pos.ToString());
			t.GetComponent<Renderer>().material.color = Color.green;
			t.transform.localScale = new Vector3(.25f, .25f, .25f);
		}
	}

}
