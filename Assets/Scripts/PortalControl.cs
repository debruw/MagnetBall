using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControl : MonoBehaviour
{
    public Transform portalOutPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            //other.GetComponent<Animator>().SetTrigger("BallDestroy");
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //other.GetComponent<BallController>().canMove = false;
            //other.transform.position = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);
            GameManager.Instance.camShake.setCameraShakeImpulseValue(1);
            other.GetComponent<TrailRenderer>().enabled = false;
            other.transform.position = new Vector3(portalOutPoint.position.x, other.transform.position.y, portalOutPoint.transform.position.z);

            //other.GetComponent<Animator>().SetTrigger("BallCreate");
        }
    }
}
