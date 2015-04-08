using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthDisplay : MonoBehaviour {

	public Slider slider;
	public Health unitHealth;
	public Text textBox;

	void Start()
	{
		unitHealth = _.playerHealth;
	}

	// Update is called once per frame
	void Update () 
	{
		if ( textBox == null) {
			GameObject temp = GameObject.Find ("PlayerHealthDisplayText");
			if ( temp != null ){
				textBox = temp.GetComponent<Text>();
			}
		} else {
			textBox.text = "<b>" + _.playerHealth.currentHealth + " / " + _.playerHealth.maximumHealth + "</b>";
		}

		if ( slider == null) {
			GameObject temp = GameObject.Find ("PlayerHealthBarDisplay");
			if ( temp != null ){
				slider = temp.GetComponent<Slider>();
			}
		} else {
			// update health value
			slider.minValue = 0;
			slider.maxValue = unitHealth.maximumHealth;
			slider.value = unitHealth.currentHealth;
		}

	}

}