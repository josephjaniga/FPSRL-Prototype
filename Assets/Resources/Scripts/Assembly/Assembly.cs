using UnityEngine;
using System.Collections;
using UnityEditor;

[System.Serializable]
public class Assembly : MonoBehaviour {

	public Transform active;
	public Transform bases;
	public Transform parts;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void unequipBase(){

		if ( active.childCount > 0 ){

			Transform activeBase = active.GetChild(0);
			emptyBaseParts(activeBase.gameObject);
			activeBase.SetParent(bases);

		}
	}

	public void emptyBaseParts(GameObject theBaseGO){
		if ( theBaseGO != null ){
			Base theBaseComponent = theBaseGO.GetComponent<Base>();
			if ( theBaseComponent != null ){
				foreach ( GameObject slotObject in theBaseComponent.concreteSlotObjects ){
					if ( slotObject != null ){
						IPartSlot slot = slotObject.GetComponent<IPartSlot>();
						if ( slot != null ){
							// get the part from the slot
							Part p = slot.getContents();
							if ( p != null ){
								// move the Part to the Parts Pile
								p.gameObject.transform.SetParent(parts);	
								// clear out the slot
								slot.emptySlot();
							}
						}
					}
				}
			}
		}
	}

}



/**
 * Custom Editor For the BASE Class
 */
[CustomEditor (typeof(Assembly))]
public class AssemblyEditor : Editor
{
	public override void OnInspectorGUI()
	{
		Assembly myScript = (Assembly)target;
		
		DrawDefaultInspector();
		
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		EditorGUILayout.BeginHorizontal();
		if(GUILayout.Button("Empty Base Parts"))
		{
			myScript.emptyBaseParts(myScript.GetComponent<Assembly>().active.GetChild(0).gameObject);
		}
		if(GUILayout.Button("Unequip Base"))
		{
			myScript.unequipBase();
		}
		EditorGUILayout.EndHorizontal();

	}
}