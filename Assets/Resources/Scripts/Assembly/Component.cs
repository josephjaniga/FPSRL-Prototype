using UnityEngine;
using System.Collections;

public class Component : MonoBehaviour {

	public ComponentTypes concreteType = ComponentTypes.Generic;
	public int damage = 0;
	public float rateOfFire = 0f;

}

public enum ComponentTypes {
				// Slots This Fits In			What Fits In This Slot
				// ---------------------------- --------------------------------
	Generic 	// Generic Slots				Anything
}