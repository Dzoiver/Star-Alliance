using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 movevect = new Vector3(0, 0, 0);
    float speed = 11f;
    void Awake()
    {
        Vector3 scale = new Vector3(0, 0, 0);
        scale.x = transform.localScale.x * Playerscript.instance.gutlingSize;
        scale.y = transform.localScale.y * Playerscript.instance.gutlingSize;
        scale.z = transform.localScale.z * Playerscript.instance.gutlingSize;
        transform.localScale = scale;
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
        if (transform.position.magnitude > 60.0f)
        {
            Destroy(gameObject);
        }

        movevect.y = transform.position.y + speed * Time.deltaTime;
        transform.position = movevect;
    }

    void OnTriggerEnter(Collider coll)
    {
        FlyingMachine e = coll.GetComponent<FlyingMachine>();
        if (e != null)
        {
            e.TakeDamage(Playerscript.instance.gutlingDamage);
        }

        Destroy(gameObject);
    }
}
