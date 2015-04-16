using UnityEngine;
using System.Collections;

public class Cleanup : MonoBehaviour {

	void OnCollisionEnter(Collision col){

		if ( col.gameObject.name != "Player" ){
			Destroy(col.gameObject);
		}

		if ( col.gameObject.name.Contains("Coin") ){
			Destroy(col.gameObject);
		}

	}

	void OnTriggerEnter(Collider col){
		
		if ( col.gameObject.name == "Player" ){
			col.gameObject.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
		}

		if ( col.gameObject.name.Contains("Coin") ){
			Destroy(col.gameObject);
		}
		
	}

}
