using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    #region Fields
    private CinemachineVirtualCamera cinemachine;
    private float mouseSens = 1.0f;

    private float xRot;
    private float yRot;
    #endregion

    #region Functions
    // Start is called before the first frame update
    private void Awake()
    {
        
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
    #endregion
}
