using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum MoveAxis
    {
        X,
        Z
    }
    public Transform[] Waypoints;
    public float speed = 2;

    public int CurrentPoint = 0;
    public MoveAxis moveAxis;

    void Update()
    {
        if (moveAxis == MoveAxis.X)
        {
            if (transform.position.x != Waypoints[CurrentPoint].transform.position.x)
            {
                transform.position = Vector3.MoveTowards(transform.position, Waypoints[CurrentPoint].transform.position, speed * Time.deltaTime);
            }

            if (transform.position.x == Waypoints[CurrentPoint].transform.position.x)
            {
                CurrentPoint += 1;
            }
            if (CurrentPoint >= Waypoints.Length)
            {
                CurrentPoint = 0;
            }
        }
        else
        {
            if (transform.position.z != Waypoints[CurrentPoint].transform.position.z)
            {
                transform.position = Vector3.MoveTowards(transform.position, Waypoints[CurrentPoint].transform.position, speed * Time.deltaTime);
            }

            if (transform.position.z == Waypoints[CurrentPoint].transform.position.z)
            {
                CurrentPoint += 1;
            }
            if (CurrentPoint >= Waypoints.Length)
            {
                CurrentPoint = 0;
            }
        }
        
    }
}
