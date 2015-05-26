using UnityEngine;
using System.Collections;

[System.Serializable]
public class Part : MonoBehaviour {
	public PartTypes concreteType = PartTypes.Generic;
	public int damage = 0;
	public float rateOfFire = 0f;
}

public enum PartTypes {
					// Slots This Fits In			What Fits In This Slot
	Inactive,		// ---------------------------- -------------------------------- ??
	Generic, 		// Generic Slots				Anything
	PowerSource,	// Generic, PowerSource			PowerSource
}