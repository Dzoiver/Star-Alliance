using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rigidbody;
    float projectileDamage = 1f;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    public void Launch(Vector2 direction, float force)
    {
        rigidbody.AddForce(direction * force);
    }

    void Update()
    {
        if (transform.position.magnitude > 70.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        FlyingMachine e = other.collider.GetComponent<FlyingMachine>();
        if (e != null)
        {
            e.TakeDamage(projectileDamage);
        }

        Destroy(gameObject);
    }
}
