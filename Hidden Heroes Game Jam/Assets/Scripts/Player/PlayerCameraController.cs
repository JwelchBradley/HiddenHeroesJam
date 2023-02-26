using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    #region Fields
    [SerializeField] private CinemachineVirtualCamera cinemachine;
    private CinemachineBasicMultiChannelPerlin cameraShake;
    private CinemachineImpulseListener cameraShakeImpulse;
    private float mouseSens = 1.0f;

    private float xRot;
    private float yRot;

    private float startingFOV = 0;

    public float Zoom
    {
        set
        {
            cinemachine.m_Lens.FieldOfView = startingFOV * value;
        }
    }

    public float CameraShake
    {
        set
        {
            cameraShake.m_FrequencyGain += value;
        }
    }
    #endregion

    #region Functions
    private void Awake()
    {
        startingFOV = cinemachine.m_Lens.FieldOfView;
        cameraShake = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        CameraLook();
    }

    private void CameraLook()
    {
        yRot += Input.GetAxisRaw("Mouse X") * mouseSens;
        xRot -= Input.GetAxisRaw("Mouse Y") * mouseSens;

        if(yRot > 360)
        {
            yRot -= 360;
        }
        else if(yRot < -360)
        {
            yRot += 360;
        }

        xRot = Mathf.Clamp(xRot, -90, 90);

        transform.rotation = Quaternion.Euler(new Vector3(xRot, yRot));
    }

    public void ShakeImpulse()
    {
        cameraShakeImpulse.Invoke("", 1.0f);
    }
    #endregion
}
