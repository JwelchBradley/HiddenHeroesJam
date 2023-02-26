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

    private float spawnProtectionTime = 0.02f;
    private float spawnTime;

    private AudioSource audioSource;
    private Transform playerTransform;
    private bool hasHit = false;
    private bool isReturning = false;
    private Rigidbody rigidbody;
    private Collider col;
    #endregion

    #region Functions
    private void Awake()
    {
        spawnTime = Time.time;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
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
        else if(spawnTime+spawnProtectionTime < Time.time)
        {
            if (isReturning)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    var axeWeapon = FindObjectOfType<AxeWeapon>();
                    axeWeapon.ShowClub(true);
                    axeWeapon.ReturnSound();
                    Destroy(gameObject);
                }
            }
            else
            {
                if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Ax"))
                {
                    if(other.gameObject.TryGetComponent(out CollisionObjects colObj))
                    {
                        colObj.Triggered();
                    }

                    audioSource.Stop();

                    Return();
                    StartCoroutine(WaitToReturn());
                }
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
        audioSource.Play();

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
