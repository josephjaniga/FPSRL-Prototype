using UnityEngine;
using System.Collections;

public class AStarNode {

    // pathing data
    public AStarNode parent = null;

    // eight cardinal directions
    public AStarNode north      = null;
    public AStarNode northEast  = null;
    public AStarNode east       = null;
    public AStarNode southEast  = null;
    public AStarNode south      = null;
    public AStarNode southWest  = null;
    public AStarNode west       = null;
    public AStarNode northWest  = null;

    public int HV_cost = 10;    // horizontal or vertical motion cost
    public int D_cost = 14;     // diagonal motion cost

    public float unitSize = 1.5f;   // the grid spacing

    public bool walkable = true;
    public int F; // F-score
	public int G; // movement cost
	public int H; // hueristic cost

	public Vector3 pos = new Vector3(0f,0f,0f);

	public string nodeName = "";

}
