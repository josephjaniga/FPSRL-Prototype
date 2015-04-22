using UnityEngine;
using System.Collections;

using UnityStandardAssets.Characters.FirstPerson;

public class Attributes : MonoBehaviour {

    public int baseDamage = 0;
    public float baseCriticalChance = 0f;
    public float baseCriticalDamage = 0.5f;

    // TODO: how to build upon this base
    // TODO: Reconsider Rate of Fire / Attack Speed Algorithm
    public float baseROF = 1f;

    // FIXME: relationship between attributes and equipment when calculating stuff?
    // combat calls attributes? -> calls equipment

	public int movementWalkSpeed = 5;
	public int movementRunSpeed = 10;
	public int jumpSpeed = 12;

	public FirstPersonController fpc;

	void Start()
	{
		fpc = _.player.GetComponent<FirstPersonController>();
	}

	void Update()
	{
		fpc.setSpeeds(movementWalkSpeed, movementRunSpeed, jumpSpeed);
	}

    public Damage calculatedDamage
    {
        get
        {
            Damage damageObject;
            int calcDmg = 0;
            float critDmgBonus = 0f;
            bool isCrit = false;
            DamageTypes dmgType = DamageTypes.Ranged;
            Elements dmgElement = Elements.Physical;

            switch (_.playerEquipment.equippedWeapon)
            {
                default:
                    break;
                case Weapons.Pistol:
                    float calcCritChance = baseCriticalChance + _.playerEquipment._pistolCritChance + _.playerEquipment.pistolCritLevel * Equipment.STEP_pistolCritChance;
                    float calcCritDamage = baseCriticalDamage + _.playerEquipment._pistolCritDamage + _.playerEquipment.pistolCritDamageLevel * Equipment.STEP_pistolCritDamage;
                    calcDmg = _.playerEquipment._pistolBaseDamage + baseDamage;
                    calcDmg += _.playerEquipment.pistolDamageLevel * Equipment.STEP_pistolDamage;
                    critDmgBonus = _.playerAttributes.rollCrit(calcCritChance, calcCritDamage);
                    if (critDmgBonus > 1f) { isCrit = true; }
                    calcDmg = Mathf.RoundToInt(calcDmg * critDmgBonus);
                    dmgType = _.playerEquipment._pistolType;
                    dmgElement = _.playerEquipment._pistolElement;
                    break;
                case Weapons.Sword:
                    break;
            }

            // Send Message and Unity for more expansive damage object
            damageObject = new Damage(calcDmg, dmgType, dmgElement, isCrit);

            // TODO: apply weapon effects
            return damageObject;
        }
    }

    public float calculatedROF
    {
        get
        {
            float rateOfFire = 1f;

            switch (_.playerEquipment.equippedWeapon)
            {
                default:
                    break;
                case Weapons.Pistol:
                    rateOfFire = baseROF / (_.playerEquipment._pistolROF + (_.playerEquipment.pistolROFLevel * Equipment.STEP_pistolROF));
                    break;
                case Weapons.Sword:
                    break;
            }

            return rateOfFire;
        }
    }
	
	public float rollCrit(float critChance, float critDamage)
	{
		float damageMultilplier = 1f;
		if (Random.Range(0f, 1f) <= critChance)
		{
			damageMultilplier += critDamage;
		}
		return damageMultilplier;
	}

}
