using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Fields
    private Rigidbody rigidbody;
    private Transform cameraTransform;

    private float currentGravity = 0.0f;

    [SerializeField] private float playerSpeed = 7.5f;
    [SerializeField] private float maxSlope = 60.0f;
    [SerializeField] private float jumpVelocity = 10.0f;

    [SerializeField] private float extraGravity = 10.0f;

    [SerializeField] private Transform movementTransform;
    [SerializeField] private SphereCollider groundCheckCollider;


    private Vector3 normal;
    private List<GameObject> contactObjects = new List<GameObject>(); 
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
        Jump();
    }

    private void MovePlayer()
    {
        movementTransform.rotation = Quaternion.Euler(new Vector3(0, cameraTransform.rotation.eulerAngles.y, 0));

        var xMove = Input.GetAxisRaw("Horizontal");
        var yMove = Input.GetAxisRaw("Vertical");

        if (contactObjects.Count != 0)
        {
            var vel = (movementTransform.transform.forward * yMove + movementTransform.transform.right * xMove).normalized;
            var planeMovement = Vector3.ProjectOnPlane(vel, normal).normalized * playerSpeed;
            rigidbody.velocity = planeMovement;
        }
        else
        {
            var vel = (movementTransform.transform.forward * yMove + movementTransform.transform.right * xMove).normalized * playerSpeed;
            vel.y = rigidbody.velocity.y - extraGravity*Time.fixedDeltaTime;
            rigidbody.velocity = vel;
        }
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Collider[] colliders = Physics.OverlapSphere(groundCheckCollider.transform.position, groundCheckCollider.radius);

            foreach(Collider col in colliders)
            {
                if (!col.gameObject.CompareTag("Player"))
                {
                    print(col.gameObject.tag);
                    var vel = rigidbody.velocity;
                    vel.y = jumpVelocity;
                    rigidbody.velocity = vel;
                    break;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        contactObjects.Add(collision.gameObject);

        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Angle(contact.normal, Vector3.up) < maxSlope)
            {
                normal = contact.normal;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        contactObjects.Remove(collision.gameObject);

        if (contactObjects.Count == 0)
        {
            normal = Vector3.zero;
        }
    }
    #endregion
}
