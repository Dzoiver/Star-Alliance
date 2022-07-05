using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : Enemy
{
    Rigidbody _rb;
    float currentHealth = 10f;
    float maxHealth = 10f;
    float speed = 2f;
    int scoreAmount = 500;
    Vector3 movevect;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        healthBar.SetMaxHealth(currentHealth);
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
        GameDirector.enemyCount--;
        // Waypoints.instance.RestoreWaypoint(movevect);
        /*float rndChance = Random.Range(0f, 1f);
        if (rndChance < potionChance)
        {
            DropItem(potionPrefab);
        }
        else if (rndChance < tempDmgChance * 2)
        {
            DropItem(TempDmgPrefab);
        }*/

        Score.instance.AddScore(scoreAmount);
    }

    public EnemyHPBar healthBar;
    public override void TakeDamage(float damage)
    {   
        currentHealth -= damage;
        Debug.Log(currentHealth);
        healthBar.SetHealth(currentHealth);
    }
}
