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
	
	public List<Module> openModuleList = new List<Module>();
	
	// Use this for initialization 
	void Start () {

        // make the two peices
		GameObject concreteOne = Instantiate(getRandomPrefab(), new Vector3(0f,0f,0f), Quaternion.identity) as GameObject;
		theNexus = concreteOne.GetComponent<Module>();
		openModuleList.Add(theNexus);
		currentRoomSize++;

		while ( nextOpenJointFromList() != null && currentRoomSize < maxRooms ){
			Joint j = nextOpenJointFromList();
			GameObject newPeice = Instantiate(getRandomPrefab(), new Vector3(5f*currentRoomSize,-5f*currentRoomSize,0f), Quaternion.identity) as GameObject;
			Module newModule = newPeice.GetComponent<Module>();
			connectModuleToJoint(newModule, j);
			if ( newModule.getOpenJoint() != null ){
				openModuleList.Add(newModule);
			}
			newModule = null;
			newPeice = null;
			currentRoomSize++;
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
	public void connectModuleToJoint(Module theNewWork, Joint targetJoint)
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

		// close the joints
		targetJoint.isOpen = false;
		newWorkJoint.gameObject.GetComponent<Joint>().isOpen = false;

		// debugging
		GameObject prefabTrail = Resources.Load("Prefabs/_Pointer") as GameObject;
		GameObject concreteTrail = Instantiate(prefabTrail, Vector3.zero, Quaternion.identity) as GameObject;
		concreteTrail.transform.SetParent(GameObject.Find ("Effects").transform);
		concreteTrail.GetComponent<LineRenderer>().SetPosition(0, newWorkJoint.transform.position);
		concreteTrail.GetComponent<LineRenderer>().SetPosition(1, targetJoint.transform.position);


	}

	// angle alignment assistance
	private static float Azimuth(Vector3 vector)
	{
		return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
	}

}
