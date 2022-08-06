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
    float attackRate = 3f;
    Vector3 movevect;
    float attackChance = 0.5f; // 50%
    bool attackFinished = false;

    float currentTime = 3f;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        healthBar.SetMaxHealth(currentHealth);
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= attackRate && transform.position.z == 0 && attackFinished)
        {
            attackFinished = false;
            RandomAttack();
            currentTime = 0f;
        }

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void RandomAttack()
    {
        float randomChance = Random.Range(0f, 1f);
        if (randomChance < attackChance)
        {
            SpinFast();
        }
        else
        {
            SpinAndShoot();
        }
    }

    void SpinFast()
    {
        attackFinished = true;
    }

    void SpinAndShoot()
    {
        attackFinished = true;
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
