using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollide : MonoBehaviour
{
    public float damage;

    float cooldown;

    public bool onTrigger;
    public bool shouldDestroy;

    private void OnCollisionEnter(Collision collision)
    {
        Collided(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (onTrigger)
        {
            Collided(other);
        }
    }

    void Collided(Collider collision)
    {
        if (Time.time > cooldown)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Damageable>().UpdateHealth(-damage);
                cooldown = Time.time + 0.5f;
                if (shouldDestroy)
                    Destroy(gameObject);
            }
        }
    }
}
