using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    float projectileDamage = 1f;
    Rigidbody rigidbody;

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
        Playerscript p = other.collider.GetComponent<Playerscript>();
        if (p != null)
        {
            p.TakeDamage(projectileDamage);
        }

        Destroy(gameObject);
    }
}
