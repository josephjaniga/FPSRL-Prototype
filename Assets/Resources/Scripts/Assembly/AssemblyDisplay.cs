using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AssemblyDisplay : MonoBehaviour {

	public Transform active;

	public Text baseTextBox;
	public Text baseDamageTextBox;
	public Text baseROFTextBox;
	
	void Update(){

		// the text
		if ( active.childCount > 0 ){
			Transform activeBase = active.GetChild(0);
			baseTextBox.text = activeBase.name;
			baseDamageTextBox.text = activeBase.gameObject.GetComponent<Base>().damage + " damage";
			baseROFTextBox.text = activeBase.gameObject.GetComponent<Base>().rateOfFire + " rof";
		} else {
			baseTextBox.text = "";
			baseDamageTextBox.text = "";
			baseROFTextBox.text = "";
		}


	}

}
