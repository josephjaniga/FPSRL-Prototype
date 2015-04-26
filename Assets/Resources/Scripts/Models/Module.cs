using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Module : MonoBehaviour {

	public List<Joint> jointList = new List<Joint>();

	public int numberEnemies;

	void Start(){

		init ();

	}

	public Joint getOpenJoint(){

		init ();

		Joint temp = null;

		for (int j=0; j<jointList.Count; j++){

			if ( jointList[j].isOpen ){
				temp = jointList[j];
			}

		}

		return temp;

	}
	
	public void init()
	{
		
		numberEnemies = Random.Range(0, 2);

		jointList = new List<Joint>();

		foreach ( Transform t in transform ) {
			if ( t.name.Contains("Joint") ){
				jointList.Add( t.gameObject.GetComponent<Joint>() );
			}
		}
	}

	public Transform getFirstRoomGeometry(){
		Transform temp = null;
		foreach (Transform Child in gameObject.transform ){
			if ( Child.gameObject.GetComponent<Collider>() != null ){
				temp = Child;
				break;
			}
		}
		return temp;
	}

	public void addExit()
	{
		Transform Geometry = getFirstRoomGeometry();

		Vector3 localPosition = new Vector3(
			Random.Range(-.5f, .5f) * Geometry.localScale.x,
			0f,
			Random.Range(-.5f, .5f) * Geometry.localScale.z
		);
		Vector3 location = gameObject.transform.position + new Vector3(0f, gameObject.transform.localScale.y+0.25f, 0f);
		GameObject exitPrefab = Resources.Load("Prefabs/Exit") as GameObject;
		GameObject exit = Instantiate(exitPrefab, location + localPosition, Quaternion.identity) as GameObject;

		exit.transform.SetParent(gameObject.transform);
	}

	public void spawnEnemy()
	{
		Transform Geometry = getFirstRoomGeometry();
		
		Vector3 localPosition = new Vector3(
			Random.Range(-.5f, .5f) * Geometry.localScale.x,
			Random.Range(0f, 2f) * Geometry.localScale.y,
			Random.Range(-.5f, .5f) * Geometry.localScale.z
			);
		Vector3 location = gameObject.transform.position + new Vector3(0f, gameObject.transform.localScale.y+0.25f, 0f);
		GameObject enemyPrefab = Resources.Load("Prefabs/Characters/RiggedRobot") as GameObject;
		GameObject enemy = Instantiate(enemyPrefab, location + localPosition, Quaternion.identity) as GameObject;

		Health enemyHealth = enemy.GetComponent<Health>();
		LootTable enemyLootTable = enemy.GetComponent<LootTable>();
		enemyHealth.currentHealth = enemyHealth.maximumHealth = Mathf.RoundToInt(enemyHealth.maximumHealth * _.surveyor.maxRooms/2f);
		enemyLootTable.cashDropAmount *= Mathf.RoundToInt( _.surveyor.maxRooms / 3f );

		enemy.transform.SetParent(_.mobs.transform);
	}

}
