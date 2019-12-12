using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightLight : MonoBehaviour
{
    Rigidbody rb;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0f, 0f, speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.z < -10f) transform.position = new Vector3(0, 0, 210f);
    }
}
