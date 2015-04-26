using UnityEngine;
using System.Collections;

public class AttackTargetWithinRange : MonoBehaviour {

	public float range = 2f;
	Animator anim;

	public float attackSpeed = 2f;
	public float lastAttack = 0f;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		GameObject target = gameObject.GetComponent<Target>().target;

		if ( target != null ){

			float distToTarget = Vector3.Distance(transform.position, target.transform.position);
			
			if ( distToTarget <= range && lastAttack + attackSpeed <= Time.time ){

				lastAttack = Time.time;

				// execute one attack animation
				anim.SetBool("Attacking", true);

				Damage damageObject = new Damage(1, DamageTypes.Melee);

				// apply one attack worth of damage
				target.GetComponent<Health>().takeDamage(damageObject);

			} else {

				anim.SetBool("Attacking", false);

			}

		}

	}
}
