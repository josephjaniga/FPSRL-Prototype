using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class Base : MonoBehaviour {
	
	public List<ComponentSlot> concreteComponents = new List<ComponentSlot>();
	public IAttackMechanism concreteAttackMechanism;

	public int _damage = 1;
	public float _rateOfFire = 1f;

	int damage {
		get {
			return _damage + calculateComponentDamage();
		}
	}

	float rateOfFire{
		get {
			return _rateOfFire + calculateComponentRateOfFire();
		}
	}
	
	public int calculateComponentDamage(){
		int tally = 0;
		foreach( ComponentSlot cs in concreteComponents ){
			if ( cs != null ) {
				tally += cs.concreteComponent.damage;
			}
		}
		return tally;
	}

	public float calculateComponentRateOfFire(){
		float tally = 0f;
		foreach( ComponentSlot cs in concreteComponents ){
			if ( cs != null ) {
				tally += cs.concreteComponent.rateOfFire;
			}
		}
		return tally;
	}

	public void DebugDamage(){
		Debug.Log(gameObject.name + " Damage: " + damage);
	}

	public void DebugRateOfFire(){
		Debug.Log(gameObject.name + " RateOfFire: " + rateOfFire);
	}

}

[CustomEditor (typeof(Base))]
public class BaseEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		Base myScript = (Base)target;
		if(GUILayout.Button("Display Damage"))
		{
			myScript.DebugDamage();
		}
		if(GUILayout.Button("Display RateOfFire"))
		{
			myScript.DebugRateOfFire();
		}
	}
}