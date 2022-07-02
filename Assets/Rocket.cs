using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Vector3 movevect = new Vector3(0, 0, 0);
    float speed;
    float hozSpeed;
    Vector3 player;
    // Start is called before the first frame update
    private void Awake()
    {
        transform.position = Playerscript.instance.transform.position;
    }
    void Start()
    {
        speed = Random.Range(6f, 7f);
        hozSpeed = Random.Range(-4f, 4f);
        movevect.x = transform.position.x;
        movevect.y = transform.position.y;
        movevect.z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        movevect.y = transform.position.y + speed * Time.deltaTime;
        movevect.x = transform.position.x + hozSpeed * Time.deltaTime;
        transform.position = movevect;

        if (transform.position.magnitude > 60.0f)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        FlyingMachine e = coll.GetComponent<FlyingMachine>();
        if (e != null)
        {
            e.TakeDamage(Playerscript.instance.rocketDamage);
        }

        Destroy(transform.parent.gameObject);
    }
}
