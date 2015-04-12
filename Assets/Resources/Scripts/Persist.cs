using UnityEngine;
using System.Collections;

public class Persist : MonoBehaviour
{
	
	void Start () {
		persistHierarchy(gameObject);
	}
	
	public void persistHierarchy (GameObject go) {
		DontDestroyOnLoad(go);
		foreach( Transform child in go.transform ){
			persistHierarchy(child.gameObject);
		}
	}
	
}