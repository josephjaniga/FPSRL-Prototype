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

    // pistol enhancements
    public int pistolDamageEnhancements = 0;

    public int calculatedDamage
    {
        get {
            int calcDmg = 0;

            switch (equippedWeapon)
            {
                default:
                    break;
                case Weapons.Pistol:
                    calcDmg = _pistolBaseDamage;  
                    calcDmg += 
                    break;
                case Weapons.Sword:
                    break;
            }

            // roll crits

            // apply crit damage

            // apply weapon effects

            return calcDmg;
        }
    }

}

public enum Weapons
{
    Pistol,
    Sword
}