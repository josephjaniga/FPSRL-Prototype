using UnityEngine;
using System.Collections;

public class LootTable : MonoBehaviour {

	private GameObject coin;

	public int cashDropAmount = 25;
	public bool willDrop = false;
	public int dropChance = 50;

	// Use this for initialization
	void Start () {
		coin = Resources.Load ("Prefabs/Coin") as GameObject;
		if ( Random.Range (0,100) > 100 - dropChance ){
			willDrop = true;
		}
		if ( willDrop ){
			cashDropAmount = Random.Range (0, 10) + Random.Range (0, 10) + Random.Range (0, 10);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Die()
	{

		if ( willDrop ){
			Vector3 offset;
			Vector3 rDirection;
			Vector3 rTorque;
			GameObject temp;
			for (int i=0; i<cashDropAmount; i++) {
				// get all the offsets and torque
				offset = new Vector3(Random.Range(-1f, 1f),Random.Range(-1f, 1f),Random.Range(-1f, 1f));
				rDirection = new Vector3(Random.Range(-10f, 10f),Random.Range(-100f, 0f),Random.Range(-10f, 10f));
				rTorque = new Vector3(Random.Range(-10f, 10f),Random.Range(-10f, 10f),Random.Range(-10f, 10f));
				// spawn set parent and rename
				temp = Instantiate(coin, gameObject.transform.position + offset*0.5f, Quaternion.identity) as GameObject;
				temp.transform.SetParent(GameObject.Find ("Stuff").transform);
				temp.GetComponent<Rigidbody>().AddForce(rDirection);
				temp.GetComponent<Rigidbody>().AddTorque(rTorque);
			}
		}

	}

}
