using UnityEngine;
using System.Collections;

public class DamageForce : MonoBehaviour
{

    public enum ForceBehavior
    {
        Random,
        PositiveZ
    }

    public ForceBehavior type = ForceBehavior.PositiveZ;

    void Start()
    {

    }

	public void takeDamage(int DamageAmount)
	{

        Vector3 ForceDirecion;
        Vector3 TorqueDirecion; 

        switch (type)
        {
            default:
            case ForceBehavior.Random:
              
                ForceDirecion = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f)
                );

                TorqueDirecion = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f)
                );

            break;
            case ForceBehavior.PositiveZ:
                break;
        }
        ForceDirecion = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            );

        TorqueDirecion = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
             );

		// apply damage force
        gameObject.GetComponent<Rigidbody>().AddForce(randomForceDirecion * 100);
        gameObject.GetComponent<Rigidbody>().AddTorque(randomTorqueDirecion * 100);
	}

}
