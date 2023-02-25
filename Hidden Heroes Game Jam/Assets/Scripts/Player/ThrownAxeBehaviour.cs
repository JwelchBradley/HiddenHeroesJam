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
    [HideInInspector] public float timeBeforeGaruanteedReturn;

    private Transform playerTransform;
    private bool hasHit = false;
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

    private void Start()
    {
        StartCoroutine(ReturnFromOffMap());
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
                Destroy(gameObject);
            }
        }
        else
        {
            if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Ax"))
            {
                Return();
                StartCoroutine(WaitToReturn());
            }
        }
    }

    private IEnumerator ReturnFromOffMap()
    {
        yield return new WaitForSeconds(timeBeforeGaruanteedReturn);

        if (!hasHit)
        {
            ReturnImmediately();
        }
    }

    public void ReturnImmediately()
    {
        Return();
        StartCoroutine(ReturningRoutine());
    }

    private void Return()
    {
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        col.enabled = false;
        hasHit = true;
    }

    private IEnumerator WaitToReturn()
    {
        yield return new WaitForSeconds(timeBeforeReturning);

        yield return ReturningRoutine();
    }

    private IEnumerator ReturningRoutine()
    {
        rigidbody.constraints = RigidbodyConstraints.None;
        rigidbody.useGravity = false;
        col.enabled = true;
        isReturning = true;

        while (true)
        {
            rigidbody.MovePosition(Vector3.MoveTowards(transform.position, playerTransform.position, Time.fixedDeltaTime * returningForce));

            yield return new WaitForFixedUpdate();
        }
    }
    #endregion
}
