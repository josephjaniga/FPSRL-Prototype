using UnityEngine;
using System.Collections;

public class Damage {

    public int damageValue;
    public DamageTypes damageType;
    public Elements damageElement;
    public bool critical;
	public Vector3 damagePoint = Vector3.zero;

    public Damage(int dmg, DamageTypes type = DamageTypes.Ranged, Elements element = Elements.Physical, bool crit = false)
    {
        damageValue = dmg;
        damageType = type;
        damageElement = element;
        critical = crit;
    }

	public void setPosition(Vector3 dmgPos)
	{
		damagePoint = dmgPos;
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
    Holy,
    Dark,
    Fire,
    Water,
    Earth,
    Wind
}
