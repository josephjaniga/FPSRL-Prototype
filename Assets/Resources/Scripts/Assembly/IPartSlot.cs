using UnityEngine;
using System.Collections;

/**
 * Part Slot Interface
 */
public interface IPartSlot
{
	Part getContents();
	bool acceptsComponentType(PartTypes t);
	PartTypes getSlotType();
	void emptySlot();
}