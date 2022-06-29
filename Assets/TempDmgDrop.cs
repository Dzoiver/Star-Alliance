using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDmgDrop : MonoBehaviour
{
    float rotationSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Playerscript p = other.GetComponent<Playerscript>();
        if (p != null)
        {
            Destroy(gameObject);
            Playerscript.instance.AttackPowerup();
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
    }
}
