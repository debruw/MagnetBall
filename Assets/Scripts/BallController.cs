using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject magnet;
    Rigidbody rb;
    public float speed = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDir = magnet.transform.position - transform.localPosition;

        // Rotating in 2D Plane...
        targetDir.y = 0.0f;
        targetDir = targetDir.normalized;
        Debug.DrawRay(transform.position, targetDir, Color.red);

        //rb.AddForce(targetDir * speed);
        rb.velocity = targetDir * speed;
    }
}
