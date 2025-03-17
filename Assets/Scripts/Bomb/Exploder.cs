using UnityEngine;

public class Exploder : MonoBehaviour
{
    private int _radius = 1000;
    private int _explosionForce = 1000;

    public void Bang()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider collider in colliders)
        {
            if(collider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _radius);
            }
        }
    }
}
