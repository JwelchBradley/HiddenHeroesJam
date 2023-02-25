using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{

    public enum MoveState
    {
        COMING,
        DODGING,
        SHOOTING
    }

    public bool UFO;
    Vector3 circleRand;

    MoveState state;
    public float moveSpeed;

    public float shootCooldown;
    public float shootRange, shootRangeExtra;

    public float dodgeSpeed, dodgeLength;
    bool dodgeRight;

    float stateTimer, shootTimer;

    Transform player;
    Rigidbody rb;

    public float bulletSpeed;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").transform;

        state = MoveState.COMING;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case MoveState.COMING:
                rb.velocity = (transform.forward * moveSpeed);

                if (Vector3.Distance(transform.position, player.position) < shootRange)
                {
                    state = MoveState.SHOOTING;
                    break;
                }

                break;
            case MoveState.DODGING:
                if (UFO)
                {
                    rb.velocity = (transform.right.normalized * circleRand.x + transform.up.normalized * circleRand.y) * dodgeSpeed;
                }
                else
                {
                    if (dodgeRight)
                        rb.velocity = (transform.right.normalized * dodgeSpeed);
                    else
                        rb.velocity = (transform.right.normalized * dodgeSpeed * -1);
                }

                if (Time.time > stateTimer)
                {
                    state = MoveState.SHOOTING;
                }

                break;
            case MoveState.SHOOTING:
                rb.velocity = Vector3.zero;

                if (Vector3.Distance(transform.position, player.position) > shootRange + shootRangeExtra)
                {
                    state = MoveState.COMING;
                    break;
                }

                if (Time.time > shootTimer)
                    Shoot();

                break;
        }
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
        newBullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        state = MoveState.DODGING;
        if(UFO)
            circleRand = Random.insideUnitCircle;
        dodgeRight = System.Convert.ToBoolean(Random.Range(0, 2));
        stateTimer = Time.time + dodgeLength;
        shootTimer = Time.time + shootCooldown;
    }
}
