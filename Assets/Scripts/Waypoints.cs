using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    int randomInt = 0;
    public static Waypoints instance;
    [SerializeField] List<Transform> allChildren;
    List<Vector3> positions = new List<Vector3>();

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < allChildren.Count; i++)
        {
            positions.Add(allChildren[i].position);
        }
    }
    // Start is called before the first frame update

    public Vector3 GetRandomWaypoint()
    {
        randomInt = Random.Range(0, positions.Count);
        Vector3 returnPos = positions[randomInt];
        positions.RemoveAt(randomInt);
        // allChildren[randomInt].gameObject.SetActive(false);
        return returnPos;
        //foreach (Transform child in allChildren)
        //{
        //    child.gameObject.SetActive(false);
        //}
    }
    public void RestoreWaypoint(Vector3 vect)
    {
        positions.Add(vect);
    }
}
