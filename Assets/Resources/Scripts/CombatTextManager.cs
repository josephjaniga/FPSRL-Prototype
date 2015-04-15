using UnityEngine;
using System.Collections;

public class CombatTextManager : MonoBehaviour
{

    public GameObject worldCanvas;

    void Start()
    {
        worldCanvas = _.worldCanvas;
    }

    public void CreateChatMessage(Vector3 position, string msg, float LifeTime = 5f)
    {

		Vector3 messagePosition = position;

        GameObject temp = GameObject.Instantiate(
                Resources.Load("Prefabs/UI/CombatTextPrefab"),
                messagePosition,
                Quaternion.identity
            ) as GameObject;

        temp.GetComponent<CombatTextElement>().lifeTime = LifeTime;
        temp.GetComponent<CombatTextElement>().background.text = msg;
        temp.GetComponent<CombatTextElement>().foreground.text = msg;
        temp.transform.SetParent(worldCanvas.transform);

    }

	public void CreateSCT(Vector3 position, Damage dmgObj, float LifeTime = 5f){

		Vector3 messagePosition = position;
		
		GameObject temp = GameObject.Instantiate(
			Resources.Load("Prefabs/UI/CombatTextPrefab"),
			messagePosition,
			Quaternion.identity
			) as GameObject;

		if ( dmgObj.critical ){
			temp.GetComponent<CombatTextElement>().foreground.color = Color.red;
			temp.GetComponent<RectTransform>().localScale = new Vector3(.075f,.075f,.075f);
			temp.GetComponent<CombatTextElement>().lifeTime = LifeTime*2;
			temp.GetComponent<CombatTextElement>().background.text = dmgObj.damageValue + "!";
			temp.GetComponent<CombatTextElement>().foreground.text = dmgObj.damageValue + "!";
		} else {
			temp.GetComponent<CombatTextElement>().lifeTime = LifeTime;
			temp.GetComponent<CombatTextElement>().background.text = ""+dmgObj.damageValue;
			temp.GetComponent<CombatTextElement>().foreground.text = ""+dmgObj.damageValue;
		}


		temp.transform.SetParent(worldCanvas.transform);

	}


}