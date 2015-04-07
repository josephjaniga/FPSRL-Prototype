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
		textBox.text = "<b>" + _.playerHealth.currentHealth + " / " + _.playerHealth.maximumHealth + "</b>";

		// update health value
		slider.minValue = 0;
		slider.maxValue = unitHealth.maximumHealth;
		slider.value = unitHealth.currentHealth;
	}

}