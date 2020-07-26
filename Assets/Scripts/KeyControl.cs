using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControl : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            door.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
