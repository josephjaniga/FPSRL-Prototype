using UnityEngine;
using System.Collections;

public class AttackTargetWithinRange : MonoBehaviour {

	public float range = 3f;
	Animator anim;

	public int damageValue = 1;
	public DamageTypes type = DamageTypes.Melee;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		GameObject target = gameObject.GetComponent<Target>().target;

		if ( target != null ){

			float distToTarget = Vector3.Distance(transform.position, target.transform.position);
			
			if ( distToTarget <= range ){
				anim.SetBool("Attacking", true);
			} else {
				anim.SetBool("Attacking", false);
			}

		}

	}

	public void AttemptDamage(){

		AudioSource src = gameObject.GetComponent<AudioSource>();

		src.PlayOneShot(src.clip);

		GameObject target = gameObject.GetComponent<Target>().target;

		if ( target != null ){
			float distToTarget = Vector3.Distance(transform.position, target.transform.position);
			if ( distToTarget <= range * 1.25f ){
				Damage damageObject = new Damage(damageValue, type);
				target.GetComponent<Health>().takeDamage(damageObject);
			}
		}

	}

}
