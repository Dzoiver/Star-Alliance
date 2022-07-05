using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : MonoBehaviour
{
    public GameObject rocket;
    

    float spawnTime;
    float currentTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Random.Range(0f, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > spawnTime)
        {
            rocket.SetActive(true);
        }
    }
}
