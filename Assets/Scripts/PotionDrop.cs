using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDrop : MonoBehaviour
{
    Quaternion vect = new Quaternion();
    float rotationSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        vect.x = transform.rotation.x;
        vect.y = transform.rotation.y;
        vect.z = transform.rotation.z;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Playerscript p = other.GetComponent<Playerscript>();
        if (p != null)
        {
            Destroy(gameObject);
            Potions.instance.Add();
        }
    }

    // Update is called once per frame
    void Update()
    {
        vect.y = transform.rotation.y + rotationSpeed * Time.deltaTime;
        transform.rotation = vect;
    }
}
