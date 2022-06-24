using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    static public int enemyCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log(enemyCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount <= 0)
        {
            SpawnEnemy();
        }


    }

    public GameObject enemyPrefab;
    public GameObject enemyPos;
    public GameObject waypoint;

    void SpawnEnemy()
    {
        enemyCount++;
        GameObject enemy = Instantiate(enemyPrefab, enemyPos.transform);
        FlyingMachine machine = enemy.GetComponent<FlyingMachine>();
        machine.waypoint = waypoint;
    }
}
