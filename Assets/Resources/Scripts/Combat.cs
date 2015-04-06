using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {

	public bool meleeEquipped = false;
	public bool rangeEquipped = true;
	public bool requiresAmmunition = false;
	public bool hasAmmunition = true;
	
	public Animator unitWeaponAnimator;
	public AudioSource unitWeaponAudioSource;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if ( Input.GetMouseButtonDown(0) ){

			if ( meleeEquipped ){
				// melee attack???
			}

			if ( rangeEquipped ){
				if ( !requiresAmmunition ){
					gameObject.SendMessage("Shoot", SendMessageOptions.DontRequireReceiver);
				} else if ( hasAmmunition ){
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
			Debug.Log (hit.transform.name);
			if ( hit.transform.name != gameObject.transform.name ){
				hit.transform.gameObject.SendMessage("takeDamage", 1, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

}
