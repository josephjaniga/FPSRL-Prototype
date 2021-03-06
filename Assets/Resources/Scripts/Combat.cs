﻿using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {

	public bool meleeEquipped = false;
	public bool rangeEquipped = true;
	public bool requiresAmmunition = false;
	public bool hasAmmunition = true;
	
	public Animator unitWeaponAnimator;
	public AudioSource unitWeaponAudioSource;

	public float lastAttackTime = -999f;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if ( Input.GetMouseButton(0) && lastAttackTime + _.playerAttributes.calculatedROF <= Time.time  ){

			if ( meleeEquipped ){
				// melee attack???
			}

			if ( rangeEquipped ){
				if ( !requiresAmmunition ){
					lastAttackTime = Time.time;
					gameObject.SendMessage("Shoot", SendMessageOptions.DontRequireReceiver);
				} else if ( hasAmmunition ){
					lastAttackTime = Time.time;
					gameObject.SendMessage("Shoot", SendMessageOptions.DontRequireReceiver);
				} else {
					gameObject.SendMessage("DryFire", SendMessageOptions.DontRequireReceiver);
				}
			}

		}

	}

	public void Shoot()
	{

		if ( unitWeaponAnimator != null){
			unitWeaponAnimator.SetTrigger("Fire");
		}

		if ( unitWeaponAudioSource != null ){
			unitWeaponAudioSource.Stop();
			unitWeaponAudioSource.Play();
		}

		// look to equipment and determine rate of fire & damage?

		Vector3 fwd = _.playerBulletSpawn.TransformDirection(Vector3.forward);
		RaycastHit hit;
		if ( Physics.Raycast(_.playerBulletSpawn.position, fwd, out hit) ){

			//Debug.Log (hit.transform.name);
			if ( hit.transform.name != gameObject.transform.name ){
                Damage currentAttack = _.playerAttributes.calculatedDamage;
				currentAttack.setPosition(hit.point);
                currentAttack.source = _.player;
				hit.transform.gameObject.SendMessage("takeDamage", currentAttack, SendMessageOptions.DontRequireReceiver);
				MakeBulletTrail(hit);
			}

		} else {
			// TODO: make a bullet trail in general direction
		}
	}

	public static void MakeBulletTrail(RaycastHit hit)
	{
		GameObject prefabTrail = Resources.Load("Prefabs/BulletTrail") as GameObject;
		GameObject concreteTrail = Instantiate(prefabTrail, _.playerBulletSpawn.position, Quaternion.identity) as GameObject;
		concreteTrail.transform.SetParent(_.effects.transform);
		concreteTrail.GetComponent<LineRenderer>().SetPosition(0, _.playerBulletSpawn.position);
		concreteTrail.GetComponent<LineRenderer>().SetPosition(1, hit.point);
	}

}
