using UnityEngine;
using System.Collections;

public class Equipment : MonoBehaviour {

	public int cash;

    public Weapons equippedWeapon = Weapons.Pistol;

    // weapon ownership
    public bool hasPistol = true;
    public bool hasSword = false;

    // weapon base damage
    private int _pistolBaseDamage = 1;
    private float _pistolCritChance = 0f;
    private float _pistolCritDamage = 0f;
    private int _pistolROF = 1; // shots per second
    private Elements _pistolElement = Elements.Physical;
    private DamageTypes _pistolType = DamageTypes.Ranged;

    // pistol leveling
    public int pistolDamageLevel = 0;
    public int pistolCritLevel = 0;
    public int pistolCritDamageLevel = 0;
    public int pistolROFLevel = 0;

    // pistol step values
    private static int STEP_pistolDamage = 1;
    private static float STEP_pistolCritChance = .01f;
    private static float STEP_pistolCritDamage = .25f;
    private static int STEP_pistolROF = 1;

    public Damage calculatedDamage
    {
        get {
            
            Damage damageObject;
            int calcDmg = 0;
            float critDmgBonus = 0f;
            bool isCrit = false;
            DamageTypes dmgType = DamageTypes.Ranged;
            Elements dmgElement = Elements.Physical;

            switch (equippedWeapon)
            {
                default:
                    break;
                case Weapons.Pistol:
                    float calcCritChance = _pistolCritChance + pistolCritLevel * STEP_pistolCritChance;
                    float calcCritDamage = _pistolCritDamage + pistolCritDamageLevel * STEP_pistolCritDamage;
                    calcDmg = _pistolBaseDamage;
                    calcDmg += pistolDamageLevel * STEP_pistolDamage;
                    critDmgBonus = rollCrit(calcCritChance, calcCritDamage);
                    if ( critDmgBonus > 1f ) { isCrit = true; }
                    calcDmg = Mathf.RoundToInt(calcDmg * critDmgBonus);
                    dmgType = _pistolType;
                    dmgElement = _pistolElement;
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

public enum Weapons
{
    Pistol,
    Sword
}