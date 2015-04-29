using UnityEngine;
using System.Collections;

public class AStarNode : MonoBehaviour {

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

    public float unitSize = 0.5f;   // the grid spacing

    public bool walkable = true;
    private int _F;         // F-score
    private int _G = 9999;  // movement cost
    private int _H = 9999;  // hueristic cost

    public int F
    {
        get
        {
            _F = G + H;
            return _F;
        }
    }

    public int G
    {
        get
        {
            _G = D_cost;
            return _G;
        }
    }

    public int H
    {
        get
        {
            // TODO: manhattan method
            _H = ManhattanHueristic();
            return _H;
        }
    }

    public int ManhattanHueristic()
    {
        int tempH = 0;
        // TODO: make sure the position and destination are accurate
        int xDistance = Mathf.RoundToInt(Mathf.Abs((topDownDestination.x - topDownPosition.x) / unitSize));
        int yDistance = Mathf.RoundToInt(Mathf.Abs((topDownDestination.y - topDownPosition.y) / unitSize));
        tempH += (xDistance + yDistance) * HV_cost;
        return tempH;
    }

}
