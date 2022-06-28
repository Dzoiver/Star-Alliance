using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    
    Transform[] allChildren;
    // Start is called before the first frame update
    void Start()
    {
        allChildren = GetComponentsInChildren<Transform>();
        /*for (int i = 0; i < allChildren.Length; i++)
        {

        }*/

    }

    public Vector3 getRandomWaypoint()
    {
        int randomInt = Random.Range(0, allChildren.Length);
        allChildren[randomInt].gameObject.SetActive(false);
        Debug.Log(allChildren.Length);
        allChildren = GetComponentsInChildren<Transform>();
        Debug.Log(allChildren.Length);
        return allChildren[randomInt].position;
        //foreach (Transform child in allChildren)
        //{
        //    child.gameObject.SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
