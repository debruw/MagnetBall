using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningWall : MonoBehaviour
{
    public int speed = 10;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
