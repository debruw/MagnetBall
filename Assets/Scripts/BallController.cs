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
        //transform.position = GameManager.Instance.currentLevelObject.GetComponent<LevelProperties>().PlayerPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 2)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishPoint"))
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
            GameManager.Instance.GameWin();
            Destroy(gameObject);
            SoundManager.Instance.stopSound(SoundManager.GameSounds.Electricity);
            FindObjectOfType<PlayerController>().MagnetEffect.SetActive(false);
        }
        if (other.CompareTag("Jumper"))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 100, 0));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        SoundManager.Instance.playSound(SoundManager.GameSounds.BallHit);
    }
}
