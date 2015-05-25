using UnityEngine;
using System.Collections;

public interface IAttackMechanism
{
	void Attack();
}

public class SemiautomaticAM : IAttackMechanism
{
	public void Attack(){}
}

public class AutomaticAM : IAttackMechanism
{
	public void Attack(){}	
}

public class DrawAndReleaseAM : IAttackMechanism
{
	public void Attack(){}	
}

public enum AttackMechanismTypes
{
	Semiautomatic,
	Automatic,
	DrawAndRelease
}