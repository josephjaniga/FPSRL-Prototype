using UnityEngine;
using System.Collections;

public class Damage {

    public int damageValue;
    public DamageTypes damageType;
    public Elements damageElement;
    public bool critical;

    public Damage(int dmg, DamageTypes type = DamageTypes.Ranged, Elements element = Elements.Physical, bool crit = false)
    {
        damageValue = dmg;
        damageType = type;
        damageElement = element;
        critical = crit;
    }

}

public enum DamageTypes
{
    Ranged,
    Melee
}

public enum Elements
{
    Physical,
    Ethereal,
    Light,
    Dark,
    Fire,
    Water,
    Earth,
    Wind
}
