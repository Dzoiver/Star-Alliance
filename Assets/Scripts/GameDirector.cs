using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    static public int enemyCount = 0;
    int enemyNumberSpawn = 0;
    float timewait = 0f;
    float currentwait = 0f;
    // Start is called before the first frame update
    Vector3 spawnVect = new Vector3(0, 0, 0);
    void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount <= 0)
        {
            enemyNumberSpawn = Random.Range(1, 6);

            for (int i = 0; i < enemyNumberSpawn; i++)
            {
                SpawnEnemy();
                // StartCoroutine(CoroutineSpawn());
            }
        }


    }
    IEnumerator CoroutineSpawn()
    {
        
        // float rndTime = Random.Range(0.5f, 3f);
        yield return new WaitForSeconds(5f);
        
    }

    public GameObject enemyPrefab;
    public GameObject enemyPos;
    public GameObject waypoint;

    void SpawnEnemy()
    {
        enemyCount++;
        // enemyPos.transform.position = getRandomPos();
        GameObject enemy = Instantiate(enemyPrefab, enemyPos.transform);
        FlyingMachine machine = enemy.GetComponent<FlyingMachine>();
        machine.SetVectorToMove(Waypoints.instance.GetRandomWaypoint());
    }

    Vector3 getRandomPos()
    {
        spawnVect.x = Random.Range(-10f, 10f);
        spawnVect.y = Random.Range(6f, 19f);

        return spawnVect;
    }
}
