using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody rb;
    Animation anim;
    Transform player;
    float jumpTimer, stunTimer;
    public float jumpWait;
    public float jumpLength;
    public float extraGravity;

    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
        player = GameObject.FindWithTag("Player").transform;

        GetComponentInChildren<SpriteRenderer>().GetComponent<Animator>().SetInteger("Bug", Random.Range(1,4));
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > stunTimer)
        {
            if (Time.time < jumpTimer - jumpWait)
            {
                Vector3 moveDir = player.position - transform.position;
                moveDir.y = 0;
                moveDir = moveDir.normalized;
                rb.velocity = moveDir * moveSpeed;
            }
            else
                rb.velocity = Vector3.down * extraGravity;

            if (Time.time >= jumpTimer)
                Jump();
        }
    }

    void Jump()
    {
        jumpTimer = Time.time + jumpLength + jumpWait;
        anim.Play();
    }

    public void Knockback(float force, float stunTime = 1f)
    {
        jumpTimer = Time.time + stunTime;

        rb.velocity = transform.forward * -force;
    }
}
