using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FloatingJoystick floatingJoystick;
    private float AxisX;         // The current value of the movement input.
    private float AxisY;             // The current value of the turn input.
    public float m_Speed = 20f;                 // How fast the tank moves forward and back.
    public float oldY = 0;
    Vector3 dir, movementVector = Vector3.right;
    float angle;

    // Update is called once per frame
    void Update()
    {
        // Store the value of both input axes.
        AxisX = floatingJoystick.Horizontal;
        AxisY = floatingJoystick.Vertical;

        //Get Joystick Control
        float translationX = AxisX;
        float translationY = AxisY;

        dir = new Vector3(translationX, 0, translationY);
        angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        if (dir != Vector3.zero)
        {
            StartCoroutine(WaitAndCloseHelpers());
            movementVector = dir;
            Quaternion qua = Quaternion.AngleAxis(angle - 90, Vector3.down);
            transform.rotation = Quaternion.Slerp(transform.rotation, qua, Time.deltaTime * m_Speed);            
        }
    }
    IEnumerator WaitAndCloseHelpers()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.SwipeHelper.SetActive(false);
        GameManager.Instance.TeleportHelper.SetActive(false);
        GameManager.Instance.KeyHelper.SetActive(false);
        GameManager.Instance.ReverseHelper.SetActive(false);
        GameManager.Instance.MFieldHelper.SetActive(false);
    }

    public GameObject MagnetEffect;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            MagnetEffect.SetActive(true);
            SoundManager.Instance.playSound(SoundManager.GameSounds.Electricity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            MagnetEffect.SetActive(false);
            SoundManager.Instance.stopSound(SoundManager.GameSounds.Electricity);
        }        
    }

    public MeshRenderer magnetMeshRend;
    public GameObject reverseEffect;
    public void ChangeMagnetColor(Color clr)
    {
        magnetMeshRend.material.SetColor("_Color1_T", clr);
        reverseEffect.SetActive(true);
    }
}
