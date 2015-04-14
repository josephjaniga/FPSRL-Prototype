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

        Vector3 messagePosition = new Vector3(position.x, position.y, -1f);

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


}