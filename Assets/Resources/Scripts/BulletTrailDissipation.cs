using UnityEngine;
using System.Collections;

public class BulletTrailDissipation : MonoBehaviour {

	private float startTime = 0f;
	private float lifeTime = 6f;
	private float alpha = 0.5f;
	private LineRenderer line;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		line = gameObject.GetComponent<LineRenderer>();
		line.material = new Material(Shader.Find("Particles/Additive (Soft)"));
	}
	
	// Update is called once per frame
	void Update () {

		alpha -= Time.deltaTime / 7.5f;
		Color start = Color.white;
		start.a = alpha;
		Color end = Color.black;
		end.a = alpha;
		line.SetColors (start, end);

		if ( Time.time >= startTime + lifeTime ){
			Destroy (gameObject);
		}

	}
}
