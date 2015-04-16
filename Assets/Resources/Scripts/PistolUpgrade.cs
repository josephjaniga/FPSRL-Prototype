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

	public Text damageLevel;
	public Text attackSpeedLevel;
	public Text criticalChanceLevel;
	public Text criticalDamageLevel;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

		// set the costs
        damageUpgrade.text 			= "$" + Mathf.Pow(baseCost, _.playerEquipment.pistolDamageLevel);
        attackSpeedUpgrade.text 	= "$" + Mathf.Pow(baseCost, _.playerEquipment.pistolROFLevel);
        criticalChanceUpgrade.text 	= "$" + Mathf.Pow(baseCost, _.playerEquipment.pistolCritLevel);
        criticalDamageUpgrade.text 	= "$" + Mathf.Pow(baseCost, _.playerEquipment.pistolCritDamageLevel);
    
		// set the levels
		damageLevel.text 			= "" + _.playerEquipment.pistolDamageLevel;
		attackSpeedLevel.text 		= "" + _.playerEquipment.pistolROFLevel;
		criticalChanceLevel.text 	= "" + _.playerEquipment.pistolCritLevel;
		criticalDamageLevel.text 	= "" + _.playerEquipment.pistolCritDamageLevel;
	}

    public void purchaseUpgrade(ref int AttributeLevel)
    {
		int calculatedCost = (int)Mathf.Pow(baseCost, AttributeLevel);

		if ( _.playerEquipment.cash >= calculatedCost ){

			_.playerEquipment.cash -= calculatedCost;
			AttributeLevel++;

		} else {

			// not enough money!!

		}
    }

	public void purchaseDamage()		{ purchaseUpgrade(ref _.playerEquipment.pistolDamageLevel); }
	public void purchaseAttackSpeed()	{ purchaseUpgrade(ref _.playerEquipment.pistolROFLevel); }
	public void purchaseCriticalChance(){ purchaseUpgrade(ref _.playerEquipment.pistolCritLevel); }
	public void purchaseCriticalDamage(){ purchaseUpgrade(ref _.playerEquipment.pistolCritDamageLevel); }

}
