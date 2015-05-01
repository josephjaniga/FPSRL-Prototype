using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("clicked");
        }
	}
}
