using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * This class assembles a map from modules.
 */ 

public class Surveyor : MonoBehaviour {

	// TODO: Remove this add a weighted Module List (hash map, with random probabilty) and method grab random supported module by this joint
    public GameObject roomPrefab;

	public List<GameObject> modulePrefabsList;

	public Module theNexus;
	public int maxRooms = 2;
	public int currentRoomSize = 0;

	public int rejectionsUntilAbandonment = 8;
	public int rejectionAttempt = 0;

	public int maxRejections = 256;
	public int currentRejections = 0;
	
	public List<Module> openModuleList = new List<Module>();

	public Transform levelGeometry;

	// debugging toggles
	public bool debugRejects = true;
    public bool displayAStarNodes = true;

	public Transform alpha = null;
	public Transform omega = null;
	
	// Use this for initialization 
	void Start () {

		Generate ();

	}

	void OnLevelWasLoaded(){
		Generate ();
	}

	public GameObject getRandomPrefab(){
		if ( modulePrefabsList.Count > 0 ){
			int picky = Random.Range (0, modulePrefabsList.Count);
			return modulePrefabsList[picky];
		} else {
			return roomPrefab;
		}
	}

	public Joint nextOpenJointFromList(){

		for (int i = openModuleList.Count - 1; i >= 0; i--) {
			if ( openModuleList[i].getOpenJoint() != null ){
				return openModuleList[i].getOpenJoint();
			} else {
				openModuleList.RemoveAt(i);
			}
		}

		return null;

	}

	/**
	 * Takes a New Module and Positions it to match a Target Joint
	 */
	public void connectModuleToJoint(Module theNewWork, Joint targetJoint, bool debuggingLines = false)
	{
		// Target Joint position
		Transform targetPoint = targetJoint.transform;
		Joint newWorkJoint = theNewWork.getOpenJoint();

		// aligns the position and the rotations
		//theNewWork.transform.position = targetPoint.position + newWorkJoint.transform.localPosition;
		//theNewWork.transform.Rotate( new Vector3(targetPoint.rotation.x, targetPoint.rotation.y+180f, targetPoint.rotation.z) );

		// set the proper rotation
		Vector3 forwardVectorToMatch = -targetPoint.transform.forward;
		float correctiveRotation = Azimuth(forwardVectorToMatch) - Azimuth(newWorkJoint.transform.forward);
		theNewWork.transform.RotateAround(newWorkJoint.transform.position, Vector3.up, correctiveRotation);

		// set the proper translation
		Vector3 correctiveTranslation = targetJoint.transform.position - newWorkJoint.transform.position;
		theNewWork.transform.position += correctiveTranslation;

		if ( debuggingLines ){
			// debugging
			GameObject prefabTrail = Resources.Load("Prefabs/_Pointer") as GameObject;
			GameObject concreteTrail = Instantiate(prefabTrail, Vector3.zero, Quaternion.identity) as GameObject;
			concreteTrail.transform.SetParent(_.effects.transform);
			concreteTrail.GetComponent<LineRenderer>().SetPosition(0, newWorkJoint.transform.position);
			concreteTrail.GetComponent<LineRenderer>().SetPosition(1, targetJoint.transform.position);
		}

	}

	// angle alignment assistance
	private static float Azimuth(Vector3 vector)
	{
		return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
	}

	public void Reset()
	{
		currentRoomSize = 0;
		rejectionAttempt = 0;
		currentRejections = 0;
		openModuleList = new List<Module>();
		alpha = null;
		omega = null;

        // TODO: wipe the A* Nodes
        Transform aStar = _.aStar.transform;

        foreach (Transform child in aStar)
        {
            Destroy(child.gameObject);
        }

		AStarNodeManager asnm = GameObject.Find("A*").GetComponent<AStarNodeManager>();
		asnm.all = new List<AStarNode>();

	}

	public void Generate()
	{
		
		Reset();
		
		levelGeometry = _.levelGeometry.transform;
		
		// make the two peices
		GameObject concreteOne = Instantiate(roomPrefab, new Vector3(0f,0f,0f), Quaternion.identity) as GameObject;
		alpha = concreteOne.transform;
		omega = concreteOne.transform;
		theNexus = concreteOne.GetComponent<Module>();
		concreteOne.transform.SetParent(levelGeometry);

        drawAStarNodes(alpha);
		
        openModuleList.Add(theNexus);
		currentRoomSize++;
		
		while ( nextOpenJointFromList() != null && currentRoomSize < maxRooms && currentRejections < maxRejections ){
			
			Joint j = nextOpenJointFromList();
			GameObject newPeice = Instantiate(getRandomPrefab(), new Vector3(5f*currentRoomSize,-5f*currentRoomSize,0f), Quaternion.identity) as GameObject;
			Module newModule = newPeice.GetComponent<Module>();
			connectModuleToJoint(newModule, j);
			
			bool allowIt = true;
			
			// check if its colliding with any of the other "Settled" modules
			foreach ( Transform moduleGO in levelGeometry ){ // FOR EVERY LEVEL PEICE
				
				// ignore collision with target
				if ( moduleGO != j.gameObject.transform.parent.transform ){
					
					// check all the children and see if there are any collisions
					foreach ( Transform oldChild in moduleGO.transform ){ // FOR EACH COMPONENT
						Collider oldCol = oldChild.gameObject.GetComponent<Collider>();
						if ( oldCol != null ){  // IF THAT COMPONENT HAS A COLLIDER
							foreach ( Transform newChild in newModule.transform ){ // FOR EACH COMPONENT IN NEW
								Collider newCol = newChild.gameObject.GetComponent<Collider>();
								if ( newCol != null ){
									if ( newCol.bounds.Intersects( oldCol.bounds ) ){
										allowIt = false;
										break;
									}
								}
							}
						}
					}
					
				}
			}
			
			// ROOM ADDED SUCCESS
			if ( allowIt ){
				
				// add this one to the level geomtry
				newModule.transform.SetParent(levelGeometry);
				// add any of its open joints to the open list
				if ( newModule.getOpenJoint() != null ){
					openModuleList.Add(newModule);
				}
				currentRoomSize++;
				
				// close the joints
				j.isOpen = false;
				newModule.getOpenJoint().isOpen = false;
				omega = newModule.transform;

				if ( newModule.numberEnemies > 0 ){
					for ( int i=0; i < newModule.numberEnemies; i++){
						newModule.spawnEnemy();
					}
				}

                // draw a* nodes
                drawAStarNodes(newModule.transform);
				
			} else {
				
				if ( debugRejects ){
					foreach ( Transform Child in newPeice.transform ){
						MeshRenderer mr =  Child.GetComponent<MeshRenderer>();
						if ( mr != null ){
							mr.material.color = new Color(1f, 0f, 0f, 0.2f);
						}
					}
					
					newPeice.transform.SetParent( GameObject.Find ("Rejects").transform );
					newPeice.transform.Translate ( new Vector3(0f, -2f*(currentRejections+1), 0f) );
				} else {
					// delete the new and close off this joint
					Destroy(newPeice);
				}
				
				//	Debug.Log ("Joint : " + j.gameObject.name + " REJECTED: " + newPeice.name);
				
				currentRejections++;
				rejectionAttempt++;
				
				// try so many times... then close for business 
				if ( rejectionAttempt >= rejectionsUntilAbandonment ){
					// close that joint
					j.isOpen = false;
					rejectionAttempt = 0;
				}
			}
			
			newModule = null;
			newPeice = null;
			
		}	// end generation while
		
		foreach ( Transform Child in alpha ){
			MeshRenderer mr =  Child.GetComponent<MeshRenderer>();
			if ( mr != null ){
				mr.material.color = new Color(.5f, .5f, 1f);
			}
		}
		
		foreach ( Transform Child in omega ){
			MeshRenderer mr =  Child.GetComponent<MeshRenderer>();
			if ( mr != null ){
				mr.material.color = new Color(.5f, 1f, .5f);
			}
		}
		
		// add exit to omega room
		omega.gameObject.GetComponent<Module>().addExit();
		
		_.player.SendMessage("AStarInit", SendMessageOptions.DontRequireReceiver);

	}

    public void drawAStarNodes(Transform parent)
    {

		AStarNodeManager asnm = GameObject.Find("A*").GetComponent<AStarNodeManager>();

		AStarNode concreteNode = new AStarNode();

        foreach (Transform newChild in parent)
        {
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

                        if (hit)
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
							asnm.all.Add(tempNode);
                        }
                    }
                }
            }
        }

    }

}
