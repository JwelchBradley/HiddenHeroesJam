using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollide : MonoBehaviour
{
    public float damage;

    float cooldown;

    public bool shouldDestroy;

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time > cooldown)
        {
            if (collision.gameObject.tag == "Player")
            {
                print("Dealy Damge");

                collision.gameObject.GetComponent<Damageable>().UpdateHealth(-damage);
                cooldown = Time.time + 0.5f;
                if (shouldDestroy)
                    Destroy(gameObject);
            }
        }
    }
}
