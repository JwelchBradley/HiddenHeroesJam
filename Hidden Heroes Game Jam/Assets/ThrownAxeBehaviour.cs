/*****************************************************************************
// File Name :         ThrownAxeBehaviour.cs
// Author :            #AUTHOR#
// Contact :           #CONTACT#
// Creation Date :     #DATE#
// Company :           #COMPANY#
//
// Brief Description : Description of what the script does
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownAxeBehaviour : MonoBehaviour
{
    #region Fields
    [HideInInspector] public float damage;
    [HideInInspector] public float timeBeforeReturning;
    [HideInInspector] public float returningForce;

    private Transform playerTransform;
    private bool isReturning = false;
    private Rigidbody rigidbody;
    private Collider col;
    #endregion

    #region Functions
    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Damageable>().UpdateHealth(-damage);
        }

        if (isReturning)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponentInChildren<AxeWeapon>().hasThrown = false;
                Destroy(gameObject);
            }
        }
        else
        {
            if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Ax"))
            {
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                col.enabled = false;

                StartCoroutine(WaitToReturn());
            }
        }
    }

    private IEnumerator WaitToReturn()
    {
        yield return new WaitForSeconds(timeBeforeReturning);

        rigidbody.constraints = RigidbodyConstraints.None;
        rigidbody.useGravity = false;
        col.enabled = true;
        isReturning = true;

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, Time.fixedDeltaTime*returningForce);

            yield return new WaitForFixedUpdate();
        }
    }
    #endregion
}
