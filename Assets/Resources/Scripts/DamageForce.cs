using UnityEngine;
using System.Collections;

public class DamageForce : MonoBehaviour
{

    public enum ForceBehavior
    {
        Random,
        PositiveY
    }

    public ForceBehavior type = ForceBehavior.PositiveY;

    void Start()
    {

    }

	public void takeDamage(Damage DamageObject)
	{

        Vector3 ForceDirection;
        Vector3 TorqueDirection; 

        switch (type)
        {
            default:
            case ForceBehavior.Random:
              
                ForceDirection = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f)
                );

                TorqueDirection = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f)
                );

            break;
            case ForceBehavior.PositiveY:
  
                ForceDirection = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(0.5f, 2f),
                    Random.Range(-1f, 1f)
                );

                TorqueDirection = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f)
                );
                break;
        }

		// apply damage force
        gameObject.GetComponent<Rigidbody>().AddForce(ForceDirection * 30);
        gameObject.GetComponent<Rigidbody>().AddTorque(TorqueDirection * 100);
	}

}
