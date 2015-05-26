﻿using UnityEngine;
using System.Collections;

// Generic Component Slot - Everything Fits Here
[System.Serializable]
public class PowerSourceSlot : MonoBehaviour, IPartSlot
{
	public GameObject _concretePartObject;
	Part _concretePart;
	
	public Part concretePart {
		get {
			if ( transform.childCount > 1 ){
				Debug.LogWarning(">1 child Part in Slot: " + gameObject.name, this);
			}
			if ( _concretePartObject == null && transform.childCount > 0 ){
				Transform temp = transform.GetChild(0);
				if ( temp != null ){
					_concretePartObject = temp.gameObject;
				}
			}
			if ( _concretePart == null && _concretePartObject != null){
				_concretePart = _concretePartObject.GetComponent<Part>();
			}
			return _concretePart;
		}
	}

	public Part getContents(){
		return concretePart;
	}

	public bool acceptsComponentType(PartTypes t){
		bool temp = false;
		if ( t == PartTypes.PowerSource ){
			temp = true;
		}
		return temp;
	}
	
	public PartTypes getSlotType(){
		return PartTypes.PowerSource;
	}
	
	public void emptySlot(){
		_concretePartObject = null;
		_concretePart = null;
	}
}