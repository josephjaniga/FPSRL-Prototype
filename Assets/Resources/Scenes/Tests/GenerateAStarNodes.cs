using UnityEngine;
using System.Collections;

public class GenerateAStarNodes : MonoBehaviour {

	public AStarNodeManager asnm;

	// Use this for initialization
	void Start () {
		asnm = GameObject.Find("A*").GetComponent<AStarNodeManager>();
		asnm.pending++;
		Debug.Log (asnm.pending);
		generateNodes();
	}

	public void generateNodes(){

		bool displayAStarNodes = true;

		AStarNodeManager asnm = GameObject.Find("A*").GetComponent<AStarNodeManager>();
		
		AStarNode concreteNode = new AStarNode();

		GameObject newChild = gameObject;

		Collider newCol = newChild.gameObject.GetComponent<Collider>();
		if (newCol != null)
		{
			// round to the nearest node unit size
			float xMin = newCol.bounds.min.x - (newCol.bounds.min.x % concreteNode.unitSize);
			float xMax = newCol.bounds.max.x + 1 - (newCol.bounds.max.x % concreteNode.unitSize);
			float zMin = newCol.bounds.min.z - (newCol.bounds.min.z % concreteNode.unitSize);
			float zMax = newCol.bounds.max.z + 1 - (newCol.bounds.max.z % concreteNode.unitSize);
			float top = newCol.bounds.max.y;
			
			int zSteps = Mathf.RoundToInt((zMax - zMin) / concreteNode.unitSize);
			int xSteps = Mathf.RoundToInt((xMax - xMin) / concreteNode.unitSize);
			
			GameObject prefab = Resources.Load("Prefabs/aStarNode") as GameObject;

			// place the nodes
			for (int x = 0; x < xSteps; x++)
			{
				for (int z = 0; z < zSteps; z++)
				{
					// raycast down to the collider
					// if score a hit draw an A* Node @
					// new Vector3(xMin+x*0.5f, top, zMin+z*0.5f)
					Vector3 targetPoint = new Vector3(xMin + x * concreteNode.unitSize, top, zMin + z * concreteNode.unitSize);
					Vector3 sourcePoint = targetPoint + new Vector3(0f, 5f, 0f);
					
					bool hit = Physics.Raycast(sourcePoint, Vector3.down);
					
					if (hit && GameObject.Find(targetPoint.ToString()) == null )
					{
						// FIXME: there are a* nodes in places that dont seem logical
						if ( displayAStarNodes ){
							// draw the nodes
							GameObject temp = Instantiate(prefab, targetPoint, Quaternion.identity) as GameObject;
							temp.transform.SetParent(GameObject.Find("A*").transform);
							temp.name = targetPoint.ToString();
						}
						
						// populate the nodelist
						AStarNode tempNode = new AStarNode();
						tempNode.pos = targetPoint;
						tempNode.nodeName = targetPoint.ToString();

						asnm.all.Add(tempNode);
					}
				}
			}

			asnm.pending--;

			if ( asnm.pending <= 0 ){
				foreach ( AStarNode tempNode in asnm.all ){
					
					tempNode.north = 		asnm.getNodeByName((tempNode.pos + new Vector3( 0f, 0f,  1f) * concreteNode.unitSize).ToString());
					tempNode.northEast = 	asnm.getNodeByName((tempNode.pos + new Vector3( 1f, 0f,  1f) * concreteNode.unitSize).ToString());
					tempNode.east = 		asnm.getNodeByName((tempNode.pos + new Vector3( 1f, 0f,  0f) * concreteNode.unitSize).ToString());
					tempNode.southEast = 	asnm.getNodeByName((tempNode.pos + new Vector3( 1f, 0f, -1f) * concreteNode.unitSize).ToString());
					tempNode.south = 		asnm.getNodeByName((tempNode.pos + new Vector3( 0f, 0f, -1f) * concreteNode.unitSize).ToString());
					tempNode.southWest = 	asnm.getNodeByName((tempNode.pos + new Vector3(-1f, 0f, -1f) * concreteNode.unitSize).ToString());
					tempNode.west = 		asnm.getNodeByName((tempNode.pos + new Vector3(-1f, 0f,  0f) * concreteNode.unitSize).ToString());
					tempNode.northWest = 	asnm.getNodeByName((tempNode.pos + new Vector3(-1f, 0f,  1f) * concreteNode.unitSize).ToString());

				}
			}
			
		}
		
	}
	
}
