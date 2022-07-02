using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    float projectileDamage = 1f;
    float speed = 4f;
    Vector3 movevect = new Vector3(0, 0, 0);

    void Awake()
    {
        
    }
    // Start is called before the first frame update
    public void Launch(Vector2 direction, float force)
    {
        movevect.x = transform.position.x;
        movevect.y = transform.position.y;
        movevect.z = transform.position.z;
    }

    void Update()
    {
        if (transform.position.magnitude > 70.0f)
        {
            Destroy(gameObject);
        }
        movevect.y = transform.position.y - speed * Time.deltaTime;
        transform.position = movevect;
    }

/*    void OnCollisionEnter(Collision other)
    {
        Playerscript p = other.collider.GetComponent<Playerscript>();
        if (p != null)
        {
            p.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }

        SphereAttack s = other.collider.GetComponent<SphereAttack>();
        if (s != null)
        {
            Destroy(gameObject);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered a trigger");
        Playerscript p = other.GetComponent<Playerscript>();
        if (p != null)
        {
            p.TakeDamage(projectileDamage);
        }
        Destroy(gameObject);
    }
}
