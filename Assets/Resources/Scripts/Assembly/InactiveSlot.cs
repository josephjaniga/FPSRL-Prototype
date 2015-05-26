using UnityEngine;
using System.Collections;

// Inactive Component Slot - Nothing Fits Here
[System.Serializable]
public class InactiveSlot : MonoBehaviour, IPartSlot
{
	public Part getContents(){ return null; }
	public bool acceptsComponentType(PartTypes t){ return false; }
	public PartTypes getSlotType(){ return PartTypes.Inactive; }
	public void emptySlot(){ }
}