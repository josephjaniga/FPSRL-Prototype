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
    private static GameObject   _worldCanvas;
    private static CombatTextManager _combatTextManager;
	
    // the helpers
	private static Surveyor		_surveyor;
	private static GameObject	_mobs;
	private static GameObject	_effects;
	private static GameObject	_stuff;
	private static GameObject	_levelGeometry;
	private static GameObject	_mapper;


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

    public static GameObject worldCanvas
    {
        get
        {
            GameObject temp = GameObject.Find("WorldSpaceCanvas");
            if (_worldCanvas == null)
            {
                if (temp == null)
                {
                    temp = _.ui.transform.FindChild("WorldSpaceCanvas").gameObject;
                }
                _worldCanvas = temp;
            }
            return _worldCanvas;
        }
        set { _worldCanvas = value; }
    }

    public static CombatTextManager combatTextManager
    {
        get
        {
            if (_combatTextManager == null)
            {
                _combatTextManager = _.ui.GetComponent<CombatTextManager>();
            }
            return _combatTextManager;
        }
    }
	
	public static Surveyor surveyor
	{
		get {
			if ( _surveyor == null ){
				_surveyor = _.mapper.GetComponent<Surveyor>();
			}
			return _surveyor;
		}
	}


	public static GameObject mobs
	{
		get {
			if ( _mobs == null ){
				GameObject temp = GameObject.Find("Mobs");
				if ( temp == null ){
					temp = new GameObject();
					temp.name = "Mobs";
				}
				_mobs = temp;
			}
			return _mobs;
		}
		set { _mobs = value; }
	}

	public static GameObject stuff
	{
		get {
			if ( _stuff == null ){
				GameObject temp = GameObject.Find("Stuff");
				if ( temp == null ){
					temp = new GameObject();
					temp.name = "Stuff";
				}
				_stuff = temp;
			}
			return _stuff;
		}
		set { _stuff = value; }
	}

	public static GameObject effects
	{
		get {
			if ( _effects == null ){
				GameObject temp = GameObject.Find("Effects");
				if ( temp == null ){
					temp = new GameObject();
					temp.name = "Effects";
				}
				_effects = temp;
			}
			return _effects;
		}
		set { _effects = value; }
	}

	public static GameObject levelGeometry
	{
		get {
			if ( _levelGeometry == null ){
				GameObject temp = GameObject.Find("LevelGeometry");
				if ( temp == null ){
					temp = GameObject.Instantiate(Resources.Load("Prefabs/Setup/LevelGeometry"), Vector3.zero, Quaternion.identity) as GameObject;
					temp.name = "LevelGeometry";
					_levelGeometry = temp;
				}
			}
			return _levelGeometry;
		}
		set { _levelGeometry = value; }
	}

	public static GameObject mapper
	{
		get {
			if ( _mapper == null ){
				GameObject temp = GameObject.Find("Mapper");
				if ( temp == null ){
					temp = GameObject.Instantiate(Resources.Load("Prefabs/Setup/Mapper"), Vector3.zero, Quaternion.identity) as GameObject;
					temp.name = "Mapper";
					_mapper = temp;
				}
			}
			return _mapper;
		}
		set { _mapper = value; }
	}




	// UNNESSECARY???
	private static GameObject	_gameController;
	private static LevelManager	_levelManager;

	//	public static GameObject gameController
	//	{
	//		get {
	//			if ( _gameController == null ){
	//				GameObject temp = GameObject.Find("GameController");
	//				if ( temp == null ){
	//					temp = GameObject.Instantiate(Resources.Load("Prefabs/Setup/GameController"), Vector3.zero, Quaternion.identity) as GameObject;
	//					temp.name = "GameController";
	//				}
	//				_gameController = temp;
	//			}
	//			return _gameController;
	//		}
	//		set { _gameController = value; }
	//	}
	
	//	public static LevelManager levelManager
	//	{
	//		get {
	//			if ( _levelManager == null ){
	//				_levelManager = _.gameController.GetComponent<LevelManager>();
	//			}
	//			return _levelManager;
	//		}
	//	}


}
