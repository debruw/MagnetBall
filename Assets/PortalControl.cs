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
            other.GetComponent<Animator>().SetTrigger("BallDestroy");
            other.GetComponent<BallController>().canMove = false;
            StartCoroutine(WaitAndCreate(other.gameObject));
        }
    }

    IEnumerator WaitAndCreate(GameObject ball)
    {
        yield return new WaitForSeconds(.5f);
        ball.transform.position = new Vector3(portalOutPoint.position.x, ball.transform.position.y, portalOutPoint.transform.position.z);
        ball.GetComponent<Animator>().SetTrigger("BallCreate");
        ball.GetComponent<TrailRenderer>().enabled = true;
    }
}
