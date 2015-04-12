using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUIDisplay : MonoBehaviour {

	public Slider slider;
	public Health unitHealth;
	public Text healthTextBox;
	public Text cashTextBox;
	public Text levelTextBox;

	void Start()
	{
		unitHealth = _.playerHealth;
	}

	// Update is called once per frame
	void Update () 
	{
		if ( healthTextBox == null) {
			GameObject temp = GameObject.Find ("PlayerHealthDisplayText");
			if ( temp != null ){
				healthTextBox = temp.GetComponent<Text>();
			}
		} else {
			healthTextBox.text = "<b>" + _.playerHealth.currentHealth + " / " + _.playerHealth.maximumHealth + "</b>";
		}

		if ( cashTextBox == null) {
			GameObject temp = GameObject.Find ("PlayerCashDisplayText");
			if ( temp != null ){
				cashTextBox = temp.GetComponent<Text>();
			}
		} else {
			cashTextBox.text = "<b><color=green>$</color>" + _.playerEquipment.cash + "</b>";
		} 

		if ( levelTextBox == null) {
			GameObject temp = GameObject.Find ("LevelNumberDisplayText");
			if ( temp != null ){
				levelTextBox = temp.GetComponent<Text>();
			}
		} else {
			levelTextBox.text = "<b><color=grey>Level </color>" + _.surveyor.maxRooms + "</b>";
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