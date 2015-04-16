using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PistolUpgrade : MonoBehaviour {

    public int baseCost = 4;
    public int costScale;

    public Text damageUpgrade;
    public Text attackSpeedUpgrade;
    public Text criticalChanceUpgrade;
    public Text criticalDamageUpgrade;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        damageUpgrade.text = "$" + Mathf.Pow(baseCost, _.playerEquipment.pistolDamageLevel);
        attackSpeedUpgrade.text = "$" + Mathf.Pow(baseCost, _.playerEquipment.pistolROFLevel);
        criticalChanceUpgrade.text = "$" + Mathf.Pow(baseCost, _.playerEquipment.pistolCritLevel);
        criticalDamageUpgrade.text = "$" + Mathf.Pow(baseCost, _.playerEquipment.pistolCritDamageLevel);
    }

    public void upgradeWeaponDamage()
    {
        // get the current level
        // calculate the cost?
        // 
    }

}
