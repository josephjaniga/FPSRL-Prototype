using UnityEngine;
using System.Collections;

public class Advance : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{

		if ( col.gameObject.name == "Player" ){
			_.surveyor.maxRooms++;
			Application.LoadLevel("ContractorTest");
		}

	}

}
