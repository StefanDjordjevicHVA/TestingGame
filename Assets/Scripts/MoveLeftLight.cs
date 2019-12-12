using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftLight : MonoBehaviour
{
    Rigidbody rb;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0f, 0f, speed);

        if (transform.position.z < -10f) transform.position = new Vector3(0, 0, 210f);
    }
}
