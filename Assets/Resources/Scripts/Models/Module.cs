using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Module : MonoBehaviour {

	public List<Joint> jointList = new List<Joint>();

	void Start(){

		init ();

	}

	public Joint getOpenJoint(){

		init ();

		Joint temp = null;

		for (int j=0; j<jointList.Count; j++){

			if ( jointList[j].isOpen ){
				temp = jointList[j];
			}

		}

		return temp;

	}


	public void init()
	{
		jointList = new List<Joint>();

		foreach ( Transform t in transform ) {
			if ( t.name.Contains("Joint") ){
				jointList.Add( t.gameObject.GetComponent<Joint>() );
			}
		}
	}

}
