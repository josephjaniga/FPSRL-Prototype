using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarNodeManager : MonoBehaviour {

	public List<AStarNode> all = new List<AStarNode>();

    public AStarNode getNodeByName(string n)
    {
        AStarNode temp = null;

        foreach ( AStarNode node in all ){
            if (node.nodeName == n)
            {
                temp = node;
                return temp;
            }
        }

        return temp;
    }

}
