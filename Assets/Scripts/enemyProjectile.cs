using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    float projectileDamage = 1f;
    float speed = 4f;
    Vector3 target = new Vector3();
    Vector3 movevect = new Vector3(0, 0, 0);
    Vector3 normalizeDirection;
    Vector3 moveDirection;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = Playerscript.instance.GetPos();
        normalizeDirection = (target - transform.position).normalized;
    }
    void Awake()
    {
        
    }
    public void Launch(Vector3 playerLastPos, float force)
    {
        movevect.x = transform.position.x;
        movevect.y = transform.position.y;
        movevect.z = transform.position.z;
        
    }

    void Update()
    {
        if (transform.position.magnitude > 30.0f)
        {
            Destroy(gameObject);
        }
        rb.velocity = normalizeDirection * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Playerscript p = other.GetComponent<Playerscript>();
        if (p != null)
        {
            p.TakeDamage(projectileDamage);
        }
        Destroy(gameObject);
    }
}
