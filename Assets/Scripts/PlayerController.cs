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

    // Start is called before the first frame update
    void Start()
    {

    }

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
            movementVector = dir;
            Quaternion qua = Quaternion.AngleAxis(angle - 90, Vector3.down);
            transform.rotation = Quaternion.Slerp(transform.rotation, qua, Time.deltaTime * m_Speed);            
        }
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
}
