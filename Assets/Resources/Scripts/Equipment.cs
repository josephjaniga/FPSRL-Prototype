using UnityEngine;
using System.Collections;

public class Equipment : MonoBehaviour {

	public int cash;

    public Weapons equippedWeapon = Weapons.Pistol;

    // weapon ownership
    public bool hasPistol = true;
    public bool hasSword = false;

    // weapon base damage
    public int _pistolBaseDamage = 1;
    public float _pistolCritChance = .05f;
    public float _pistolCritDamage = 0f;
    public float _pistolROF = 0.5f; // shots per second
    public Elements _pistolElement = Elements.Physical;
    public DamageTypes _pistolType = DamageTypes.Ranged;

    // pistol leveling
    public int pistolDamageLevel = 0;
    public int pistolCritLevel = 0;
    public int pistolCritDamageLevel = 0;
    public int pistolROFLevel = 0;

    // pistol step values
    public static int      STEP_pistolDamage = 1;
    public static float    STEP_pistolCritChance = .05f;
    public static float    STEP_pistolCritDamage = 1.25f;
    public static float    STEP_pistolROF = 2f;

}

public enum Weapons
{
    Pistol,
    Sword
}