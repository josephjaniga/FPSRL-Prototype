using UnityEngine;
using System.Collections;

public static class _
{

	// the player singletons
	private static GameObject 	_player;
	private static Health 		_playerHealth;
	private static Combat		_playerCombat;
	private static Equipment	_playerEquipment;
	private static Transform	_playerBulletSpawn;
	private static GameObject	_playerWeapon;
	private static Transform	_playerStartPosition;

	// the UI
	private static GameObject	_ui;

	// the "Groups"
	// TODO


	// player
	public static GameObject player
	{
		get {
			GameObject temp = GameObject.Find("Player");
			if ( _player == null ){
				if ( temp == null ){
					temp = GameObject.Instantiate(Resources.Load("Prefabs/Characters/Player"), Vector3.zero, Quaternion.identity) as GameObject;
					temp.name = "Player";
				}
				_player = temp;
			}
			return _player;
		}
		set { _player = value; }
	}

	// playerHealth
	public static Health playerHealth
	{
		get {
			if ( _playerHealth == null ){
				_playerHealth = _.player.GetComponent<Health>();
			}
			return _playerHealth;
		}
	}

	// playerCombat
	public static Combat playerCombat
	{
		get {
			if ( _playerCombat == null ){
				_playerCombat = _.player.GetComponent<Combat>();
			}
			return _playerCombat;
		}
	}

	// playerEquipment
	public static Equipment playerEquipment
	{
		get {
			if ( _playerEquipment == null ){
				_playerEquipment = _.player.GetComponent<Equipment>();
			}
			return _playerEquipment;
		}
	}

	// playerBulletSpawn
	public static Transform playerBulletSpawn
	{
		get {
			if ( _playerBulletSpawn == null ){
				_playerBulletSpawn = _.player.transform.FindChild("FirstPersonCharacter").transform;
			}
			return _playerBulletSpawn;
		}
	}

	// playerWeapon
	public static GameObject playerWeapon
	{
		get {
			if ( _playerWeapon == null ){
				_playerWeapon = _.player.transform.FindChild("FirstPersonCharacter").transform.FindChild("Weapon").gameObject;
			}
			return _playerWeapon;
		}
	}

	// playerStartPosition
	public static Transform playerStartPosition
	{
		get {
			if ( _playerStartPosition == null ){
				GameObject temp = GameObject.Find("StartPosition");

				if ( temp == null ){
					_playerStartPosition = new GameObject().transform;
					_playerStartPosition.position = Vector3.zero;
				} else {
					_playerStartPosition = temp.transform;
				}
			}
			return _playerStartPosition;
		}
	}

	// UI
	public static GameObject ui
	{
		get {
			GameObject temp = GameObject.Find("UI");
			if ( _ui == null ){
				if ( temp == null ){
					temp = GameObject.Instantiate(Resources.Load("Prefabs/UI/UI"), Vector3.zero, Quaternion.identity) as GameObject;
					temp.name = "UI";
				}
				_ui = temp;
			}
			return _ui;
		}
		set { _ui = value; }
	}

}
