using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMachine : MonoBehaviour
{
    Rigidbody _rb;
    float health = 3f;
    float speed = 3f;
    float attackRate = 3f;
    float currentTime = 3f;
    public GameObject waypoint;
    Vector3 lookDirection = new Vector3(0, -1, 0);
    public EnemyHPBar healthBar;

    Vector3 movevect;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        healthBar.SetMaxHealth(health);
        // GetRandomPosition();
    }

/*    void OnCollisionEnter(Collision col)
    {
        
        if (col.gameObject.tag == "Enemy")
        {
            GetRandomPosition();
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= attackRate)
        {
            ShootMissiles();
            currentTime = 0f;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            GameDirector.enemyCount--;
            Waypoints.instance.RestoreWaypoint(movevect);
        }
        /*
        if (waypoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoint.transform.position, speed * Time.deltaTime);
        }
        */

        transform.position = Vector3.MoveTowards(transform.position, movevect, speed * Time.deltaTime);
    }
    void GetRandomPosition()
    {
        movevect = Waypoints.instance.GetRandomWaypoint();
/*        movevect.x = Random.Range(-8f, 8f);
        movevect.y = Random.Range(1f, 4f);
        movevect.z = 0;*/
    }

    public void SetVectorToMove(Vector3 vect)
    {
        movevect = vect;
    }

    public GameObject projectilePrefab;

    void ShootMissiles()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, _rb.position + Vector3.down * 0.5f, Quaternion.identity);
        enemyProjectile projectile = projectileObject.GetComponent<enemyProjectile>();
        projectile.Launch(lookDirection, 300);

        // animator.SetTrigger("Launch");
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
    }
}
