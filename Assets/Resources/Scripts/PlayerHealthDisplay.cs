using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthDisplay : MonoBehaviour {

	public Text textBox;
	
	// Update is called once per frame
	void Update () {
		textBox.text = "<b>" + _.playerHealth.currentHealth + "<color=red> / </color>" + _.playerHealth.maximumHealth + "<color=red> <3 </color></b>";
	}

}