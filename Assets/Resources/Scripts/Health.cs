using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int maximumHealth = 3;
	public int currentHealth;

	public int maximumShields = 0;
	public int currentShields;

	// Use this for initialization
	void Start () {
		currentHealth = maximumHealth;
		currentShields = maximumShields;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void takeDamage(int DamageAmount){

		// something with damage Types later?

		currentHealth -= DamageAmount;

		if ( currentHealth <= 0 ){
			gameObject.SendMessage("Die", SendMessageOptions.DontRequireReceiver);		
		}

	}

	public void Die(){
		Debug.Log("D-E-D dead bruh");
	}

}
