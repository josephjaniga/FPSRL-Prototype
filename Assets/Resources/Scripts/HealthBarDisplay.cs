using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Health))]
public class HealthBarDisplay : MonoBehaviour {

	public Health unitHealth;
	public Slider slider;
	public RectTransform healthBar;
	public Vector3 offset;

	// Use this for initialization
	void Start () {

		unitHealth = gameObject.GetComponent<Health>();

		offset = new Vector3(0f, gameObject.transform.localScale.y, 0f);
	
		if ( healthBar == null ){
			GameObject temp = Instantiate(Resources.Load ("Prefabs/UI/HealthBarDisplay"), Vector3.zero, Quaternion.identity) as GameObject;
			temp.transform.SetParent(_.ui.transform.FindChild ("WorldSpaceCanvas").transform);
			healthBar = temp.GetComponent<RectTransform>();
			slider = temp.GetComponent<Slider>();
		}

	}
	
	// Update is called once per frame
	void Update () {
		// update health value
		slider.minValue = 0;
		slider.maxValue = unitHealth.maximumHealth;
		slider.value = unitHealth.currentHealth;

		// update position and rotation
		healthBar.position = gameObject.transform.position + offset;
		healthBar.LookAt(_.player.transform);
	}


	public void Die()
	{
		Destroy (slider.gameObject);	
	}

}
