using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Fields
    private Rigidbody rigidbody;
    private Transform cameraTransform;

    [SerializeField] private float playerSpeed = 7.5f;
    [SerializeField] private Transform movementTransform;
    #endregion

    #region Functions
    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        movementTransform.rotation = Quaternion.Euler(new Vector3(0, cameraTransform.rotation.eulerAngles.y, 0));

        var xMove = Input.GetAxisRaw("Horizontal");
        var yMove = Input.GetAxisRaw("Vertical");

        rigidbody.velocity = (movementTransform.transform.forward * yMove + movementTransform.transform.right * xMove) * playerSpeed;
    }
    #endregion
}
