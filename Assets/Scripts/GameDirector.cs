using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    static public int enemyCount = 0;
    int enemyNumberSpawn = 3;
    float timewait = 0f;
    float currentwait = 1f;
    int maxEnemies = 8;
    int currentEnemies = 3;
    bool waveSpawning = false;
    // Start is called before the first frame update
    Vector3 spawnVect = new Vector3(0, 0, 0);
    void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount <= 0 && !waveSpawning)
        {
            waveSpawning = true;
        }
        if (waveSpawning && enemyCount > 1)
            currentwait += Time.deltaTime;
        else
            currentwait = 10f;

        if (enemyCount < enemyNumberSpawn && currentwait > timewait && waveSpawning)
        {
            SpawnEnemy();
            currentwait = 0f;
            timewait = Random.Range(0f, 4f);
        }
        else if (enemyCount == enemyNumberSpawn)
        {
            waveSpawning = false;
            if (enemyNumberSpawn < maxEnemies)
                enemyNumberSpawn++;
        }
    }

    public GameObject enemyPrefab;
    public GameObject enemyPos;
    public GameObject waypoint;

    [SerializeField] Transform[] enemyPosArray;

    void SpawnEnemy()
    {
        enemyCount++;
        // enemyPos.transform.position = getRandomPos();
        GameObject enemy = Instantiate(enemyPrefab, RandomPos());
        FlyingMachine machine = enemy.GetComponent<FlyingMachine>();
        machine.SetVectorToMove(Waypoints.instance.GetRandomWaypoint());
    }

    Transform RandomPos()
    {
        int randPos = Random.Range(0, enemyPosArray.Length-1);
        return enemyPosArray[randPos];
    }

    Vector3 getRandomPos()
    {
        spawnVect.x = Random.Range(-10f, 10f);
        spawnVect.y = Random.Range(6f, 19f);

        return spawnVect;
    }
}
