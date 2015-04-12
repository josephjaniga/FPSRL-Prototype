using UnityEngine;
using System.Collections;

public class LootCash : MonoBehaviour {

	public AudioSource audioSource;

	void Start(){
		audioSource = GameObject.Find("CoinAudioSourceHolder").GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider col)
	{
		if ( col.gameObject.name == "PlayerPickupRadius" ){
			_.playerEquipment.cash++;
			audioSource.Play();
			Destroy(gameObject);
		}
	}

}
