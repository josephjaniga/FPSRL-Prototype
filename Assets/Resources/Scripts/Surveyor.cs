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

	public bool debugRejects = true;

	public Transform alpha = null;
	public Transform omega = null;
	
	// Use this for initialization 
	void Start () {

		levelGeometry = GameObject.Find ("LevelGeometry").transform;

        // make the two peices
		GameObject concreteOne = Instantiate(roomPrefab, new Vector3(0f,0f,0f), Quaternion.identity) as GameObject;
		alpha = concreteOne.transform;
		omega = concreteOne.transform;
		theNexus = concreteOne.GetComponent<Module>();
		concreteOne.transform.SetParent(levelGeometry);
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

					// check all the children and see if there are any collision
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

					// closer than 5f to any other room? STOP
					//	if ( Vector3.Distance(newPeice.transform.position, moduleGO.position) < 10f ){
					//		allowIt = false;
					//	}
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
				mr.material.color = new Color(0f, 0f, 1f);
			}
		}

		foreach ( Transform Child in omega ){
			MeshRenderer mr =  Child.GetComponent<MeshRenderer>();
			if ( mr != null ){
				mr.material.color = new Color(0f, 1f, 0f);
			}
		}


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
			concreteTrail.transform.SetParent(GameObject.Find ("Effects").transform);
			concreteTrail.GetComponent<LineRenderer>().SetPosition(0, newWorkJoint.transform.position);
			concreteTrail.GetComponent<LineRenderer>().SetPosition(1, targetJoint.transform.position);
		}

	}

	// angle alignment assistance
	private static float Azimuth(Vector3 vector)
	{
		return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
	}

}
