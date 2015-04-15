using UnityEngine;
using System.Collections;

public class SCTFloat : MonoBehaviour {

	public Vector3 targetPosition;
	public Vector3 startingPosition;

	void Start()
	{
		startingPosition = gameObject.GetComponent<RectTransform>().position;
		targetPosition = startingPosition + new Vector3(Random.Range (-1f, 1f), 2f, 0f);
	}

	// Update is called once per frame
	void Update ()
	{
		gameObject.GetComponent<RectTransform>().position = Vector3.Lerp(gameObject.GetComponent<RectTransform>().position, targetPosition, 3f * Time.deltaTime);
	}

}
