using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField : MonoBehaviour
{
    bool isInRange = false;
    bool isactive = false;
    public GameObject MagneticFieldEffects;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (isactive)
            {
                GameManager.Instance._ballController.gameObject.SetActive(false);
                GameManager.Instance.GameLose();
            }
            else
            {
                StartCoroutine(WaitAndActivate());
            }
        }
    }

    IEnumerator WaitAndActivate()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log(isInRange);
        MagneticFieldEffects.SetActive(true);
        isactive = true;
        if (isInRange)
        {
            GameManager.Instance._ballController.isInMagneticField = true;
            GameManager.Instance._ballController.magneticFieldPos = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            isInRange = false;
        }
    }
}
