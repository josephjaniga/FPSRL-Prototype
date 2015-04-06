using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	private float x;
	private float y;
	private float z;

	void Start() {
		x = Random.Range (-180f, 180f);
		y = Random.Range (-180f, 180f);
		z = Random.Range (-180f, 180f);
	}

	// Update is called once per frame
	void Update () {
	
		transform.Rotate(
			new Vector3(
				x * Time.deltaTime,
				y * Time.deltaTime,
				z * Time.deltaTime
			)
     	);

	}

}
