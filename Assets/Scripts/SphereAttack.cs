using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereAttack : MonoBehaviour
{
    float attackTime = 1f;
    float lastAttackTime = 0f;
    float attackDamage = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Attack()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

   /* void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision detected");
        FlyingMachine p = other.collider.GetComponent<FlyingMachine>();
        if (p != null)
        {
            Debug.Log("Taking damage");
            p.TakeDamage(attackDamage);
        }
    }*/

    void OnTriggerEnter(Collider other)
    {
        FlyingMachine p = other.GetComponent<FlyingMachine>();
        if (p != null)
        {
            Debug.Log("Taking damage");
            p.TakeDamage(attackDamage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        lastAttackTime += Time.deltaTime;
        if (lastAttackTime > attackTime)
        {
            lastAttackTime = 0f;
            gameObject.SetActive(false);
        }


    }
}
