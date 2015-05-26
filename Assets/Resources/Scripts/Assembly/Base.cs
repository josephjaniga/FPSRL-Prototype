using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class Base : MonoBehaviour {

	public List<GameObject> concreteSlotObjects = new List<GameObject>();
	public IAttackMechanism concreteAttackMechanism;

	public int _baseDamage = 1;
	public float _baseRateOfFire = 1f;

	public int damage {
		get {
			return _baseDamage + calculateComponentDamage();
		}
	}

	public float rateOfFire{
		get {
			return _baseRateOfFire + calculateComponentRateOfFire();
		}
	}
	
	public int calculateComponentDamage(){
		int tally = 0;
		foreach( GameObject go in concreteSlotObjects ){
			if ( go != null ){
				IPartSlot slot = go.GetComponent<IPartSlot>();
				if ( slot != null && slot.getContents() != null ){
					Part c = slot.getContents().GetComponent<Part>();
					if ( c != null ) {
						tally += c.damage;
					}
				}
			}
		}
		return tally;
	}

	public float calculateComponentRateOfFire(){
		float tally = 0f;
		foreach( GameObject go in concreteSlotObjects ){
			if ( go != null ){
				IPartSlot slot = go.GetComponent<IPartSlot>();
				if ( slot != null && slot.getContents() != null ){
					Part c = slot.getContents().GetComponent<Part>();
					if ( c != null ) {
						tally += c.rateOfFire;
					}
				}
			}
		}
		return tally;
	}

	public void clearSlotList(){
		concreteSlotObjects = new List<GameObject>();
	}

	public void populateSlotList(){
		clearSlotList();
		foreach ( Transform child in transform ){
			if ( child.gameObject.GetComponent<IPartSlot>() != null ){
				Debug.Log ( child.gameObject.GetComponent<IPartSlot>().GetType().Name );
				concreteSlotObjects.Add(child.gameObject);
			}
		}
	}


	/**
	 * EDITOR DEBUGGING 
	 */
	public void DebugDamage(){
		Debug.Log(gameObject.name + " Damage: " + damage);
	}

	public void DebugRateOfFire(){
		Debug.Log(gameObject.name + " RateOfFire: " + rateOfFire);
	}

//	public void SetComponentSlotList(int x){
//		concreteComponentSlots = new List<IPartSlot>();
//		while ( concreteComponentSlots.Count < x ){
//			concreteComponentSlots.Add( new GenericComponentSlot() );
//		}
//	}
//	public void AddGenericComponentSlot(){
//		concreteComponentSlots.Add( new GenericComponentSlot() );
//	}
//	public void AddInactiveComponentSlot(){
//		concreteComponentSlots.Add( new InactiveComponentSlot() );
//	}

}

/**
 * Custom Editor For the BASE Class
 */
[CustomEditor (typeof(Base))]
public class BaseEditor : Editor
{
	public override void OnInspectorGUI()
	{
		Base myScript = (Base)target;

		DrawDefaultInspector();

		EditorGUILayout.Space();
		EditorGUILayout.Space();

		EditorGUILayout.BeginHorizontal();
			if(GUILayout.Button("Display Damage"))
			{
				myScript.DebugDamage();
			}
			if(GUILayout.Button("Display RateOfFire"))
			{
				myScript.DebugRateOfFire();
			}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Space();
		EditorGUILayout.Space();

		EditorGUILayout.BeginHorizontal();
		if(GUILayout.Button("Populate"))
		{
			myScript.populateSlotList();
		}
		EditorGUILayout.EndHorizontal();

//
//		if(GUILayout.Button("Clear Component Slot List"))
//		{
//			myScript.SetComponentSlotList(0);
//		}
//
//		EditorGUILayout.BeginHorizontal();
//			if(GUILayout.Button("Add Generic")){myScript.AddGenericComponentSlot();}
//			if(GUILayout.Button("Add Inactive")){myScript.AddInactiveComponentSlot();}
//		EditorGUILayout.EndHorizontal();

//		EditorGUILayout.Space();
//		EditorGUILayout.Space();
//
//		int i=0;
//		foreach( IPartSlot c in myScript.concreteComponentSlots ){
//			EditorGUILayout.BeginVertical();
//			EditorGUILayout.LabelField(i+"", c.GetType().Name);
//			EditorGUILayout.LabelField(c.getComponent()+"", "");
//			i++;
//			EditorGUILayout.EndVertical();
//		}

	}
}