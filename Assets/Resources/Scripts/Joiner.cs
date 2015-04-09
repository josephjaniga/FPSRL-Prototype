using UnityEngine;
using System.Collections;

public class Joiner : MonoBehaviour {

    public GameObject roomPrefab;

    private GameObject concreteOne;
    private GameObject concreteTwo;

	// Use this for initialization
	void Start () {

        // make the two peices
        concreteOne = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        concreteTwo = Instantiate(roomPrefab, new Vector3(10f,10f,10f), Quaternion.identity) as GameObject;
        
        

	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
