using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject magnet;
    Rigidbody rb;
    public float speed = 10;
    Vector3 startPosition;

    private void Awake()
    {
        canMove = false;
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        //transform.position = GameManager.Instance.currentLevelObject.GetComponent<LevelProperties>().PlayerPosition.position;
    }

    public bool canMove = true;
    public bool isReversed;
    public bool isInMagneticField;
    public Vector3 magneticFieldPos;

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (transform.position.y < 2)
            {
                Vector3 targetDir = magnet.transform.position - transform.localPosition;
                if (isInMagneticField)
                {
                    targetDir = magneticFieldPos - transform.localPosition;
                }

                // Rotating in 2D Plane...
                targetDir.y = 0.0f;
                targetDir = targetDir.normalized;

                if (isReversed)
                {
                    targetDir = -targetDir;
                }
                Debug.DrawRay(transform.position, targetDir, Color.red);


                //rb.AddForce(targetDir * speed);
                rb.velocity = targetDir * speed;
            }
        }
    }

    public PlayerController _playerController;
    public GameObject pickUpEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishPoint"))
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<Animator>().SetTrigger("BallDestroy");
            canMove = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<TrailRenderer>().enabled = false;
            transform.position = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
            GameManager.Instance.GameWin();
        }
        else if (other.CompareTag("Thorn"))
        {
            GetComponent<Animator>().SetTrigger("BallDestroy");
            GameManager.Instance.camShake.setCameraShakeImpulseValue(1);
            GameManager.Instance.GameLose();
        }
        else if (other.CompareTag("Coin"))
        {
            GameManager.Instance.CollectedCoinCount++;
            other.gameObject.SetActive(false);
            GameManager.Instance.camShake.setCameraShakeImpulseValue(2);
            Instantiate(pickUpEffect, transform.position, Quaternion.identity);
        }
        else if (other.CompareTag("Reverse"))
        {
            isReversed = true;
            other.gameObject.SetActive(false);
            _playerController.ChangeMagnetColor(Color.blue);
            Instantiate(pickUpEffect, transform.position, Quaternion.identity);
        }
    }

    public void BallDestroy()
    {
        //gameObject.SetActive(false);
        SoundManager.Instance.stopSound(SoundManager.GameSounds.Electricity);
        FindObjectOfType<PlayerController>().MagnetEffect.SetActive(false);
    }

    public void MakeMove()
    {
        canMove = true;
        GetComponent<TrailRenderer>().enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        SoundManager.Instance.playSound(SoundManager.GameSounds.BallHit);
        GameManager.Instance.camShake.setCameraShakeImpulseValue(2);
    }
}
