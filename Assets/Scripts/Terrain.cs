using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Terrain : MonoBehaviour
{
    float speed = 5f;


    // Start is called before the first frame update
    public ProBuilderMesh mesh;
    Bounds b;
    float meshWidth = 0f;

    void Start()
    {
        MeshRenderer x = GetComponent<MeshRenderer>();
        b = x.GetComponent<MeshFilter>().mesh.bounds;
        meshWidth = b.extents.x * 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
        // Vector2 position = _rb.position;
        // transform.position.x = transform.position.x + speed * Time.deltaTime;
        if (transform.position.y <= -50)
        {
            // Debug.Log(transform.width);
            Vector3 move = transform.position;
            move.y += meshWidth * 2;
            transform.position = move;
        }

        Vector3 vect = transform.position;
        vect.y = transform.position.y - speed * Time.deltaTime;
        transform.position = vect;

        
        // position.x = position.x + speed * Time.deltaTimes;
    }
}
