using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        Vector3 scale = new Vector3(0, 0, 0);
        scale.x = transform.localScale.x * Playerscript.instance.gutlingSize;
        scale.y = transform.localScale.y * Playerscript.instance.gutlingSize;
        scale.z = transform.localScale.z * Playerscript.instance.gutlingSize;
        transform.localScale = scale;
    }
    // Start is called before the first frame update
    public void Launch(Vector2 direction, float force)
    {
        rigidbody.AddForce(direction * force);
    }

    void Update()
    {
        if (transform.position.magnitude > 60.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        FlyingMachine e = other.collider.GetComponent<FlyingMachine>();
        if (e != null)
        {
            e.TakeDamage(Playerscript.instance.gutlingDamage);
        }

        Destroy(gameObject);
    }
}
