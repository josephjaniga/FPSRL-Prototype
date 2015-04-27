using UnityEngine;
using System.Collections;

public class MoveTowardTarget : MonoBehaviour {

	public float speed = 0.1f;
	public bool crouch = false;
	Animator anim;

	void Start() {
		anim = gameObject.GetComponent<Animator>();
		if ( Random.Range(0f, 1f) > 0.5f ){
			crouch = false;
		}
		anim.SetBool("Crouch", crouch);
	}

	// Update is called once per frame
	void Update () {
	
		if ( gameObject.GetComponent<Target>().target != null && !anim.GetBool("Attacking") ){

			anim.SetFloat("Forward", speed);

		} else {

			anim.SetFloat("Forward", 0.0f);

		}

	}
}
