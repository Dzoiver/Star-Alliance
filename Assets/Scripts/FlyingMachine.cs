﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMachine : MonoBehaviour
{
    Rigidbody _rb;
    float health = 4f;
    float speed = 4f;
    float attackRate = 3f;
    float currentTime = 3f;
    public GameObject waypoint;
    public EnemyHPBar healthBar;
    float potionChance = 0.05f;
    float tempDmgChance = 0.05f;
    int scoreAmount = 100;

    Vector3 movevect;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        healthBar.SetMaxHealth(health);
        // GetRandomPosition();
    }
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= attackRate && transform.position.z == 0)
        {
            ShootMissiles();
            currentTime = 0f;
        }

        if (health <= 0)
        {
            Death();
        }

        transform.position = Vector3.MoveTowards(transform.position, movevect, speed * Time.deltaTime);
    }

    void Death()
    {
        Destroy(gameObject);
        GameDirector.enemyCount--;
        Waypoints.instance.RestoreWaypoint(movevect);
        float rndChance = Random.Range(0f, 1f);
        if (rndChance < potionChance)
        {
            DropItem(potionPrefab);
        }
        else if (rndChance < tempDmgChance * 2)
        {
            DropItem(TempDmgPrefab);
        }

        Score.instance.AddScore(scoreAmount);
    }
    public GameObject potionPrefab;
    public GameObject TempDmgPrefab;
    void DropItem(GameObject prefab)
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
    void GetRandomPosition()
    {
        movevect = Waypoints.instance.GetRandomWaypoint();
    }

    public void SetVectorToMove(Vector3 vect)
    {
        movevect = vect;
    }

    public GameObject projectilePrefab;

    void ShootMissiles()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, _rb.position + Vector3.down * 0.5f, Quaternion.identity);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
    }
}
