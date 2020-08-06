using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float fCamShakeImpulse = 0.0f;  //Camera Shake Impulse

    private void FixedUpdate()
    {
        //make the camera shake if the fCamShakeImpulse is not zero
        if (fCamShakeImpulse > 0.0f)
            shakeCamera();
    }

    /*
    *	FUNCTION: Set the intensity of camera vibration
    *	PARAMETER 1: Intensity value of the vibration
    */
    public void setCameraShakeImpulseValue(int iShakeValue)
    {
        if (iShakeValue == 1)
            fCamShakeImpulse = 0.2f;
        else if (iShakeValue == 2)
            fCamShakeImpulse = 0.02f;
        else if (iShakeValue == 3)
            fCamShakeImpulse = 1.3f;
        else if (iShakeValue == 4)
            fCamShakeImpulse = 1.5f;
        else if (iShakeValue == 5)
            fCamShakeImpulse = 1.3f;
    }

    /*
    *	FUNCTION: Make the camera vibrate. Used for visual effects
    */
    public void shakeCamera()
    {
        transform.position += new Vector3(0, Random.Range(-fCamShakeImpulse, fCamShakeImpulse), Random.Range(-fCamShakeImpulse, fCamShakeImpulse));
        fCamShakeImpulse -= Time.deltaTime * fCamShakeImpulse * 6.0f;
        if (fCamShakeImpulse < 0.01f)
            fCamShakeImpulse = 0.0f;
    }

}
