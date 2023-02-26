using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    Rigidbody rb;
    Transform target;

    public float moveSpeed;

    [Range(0f,1f)]
    public float damping;

    public Vector3 spawnForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").transform;

        rb.velocity = spawnForce;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 targetVel = (target.position - transform.position).normalized * moveSpeed;
            rb.velocity = Vector3.Lerp(rb.velocity, targetVel, damping);
        }
    }
}
