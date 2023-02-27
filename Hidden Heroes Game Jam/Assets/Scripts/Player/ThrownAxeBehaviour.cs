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
using System.Linq;
using UnityEngine;

public class ThrownAxeBehaviour : MonoBehaviour
{
    #region Fields
    [HideInInspector] public float damage;
    [HideInInspector] public float timeBeforeReturning;
    [HideInInspector] public float returningForce;
    [HideInInspector] public float timeBeforeGaruanteedReturn;

    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip hitEnemySound;

    private float spawnProtectionTime = 0.02f;
    private float spawnTime;
    private Queue<Vector3> pastPositions = new Queue<Vector3>();

    private Quaternion startingRotation;
    private Vector3 angularVelocity = Vector3.zero;
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
        startingRotation = transform.rotation;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void Start()
    {
        angularVelocity = rigidbody.angularVelocity;
        StartCoroutine(ReturnFromOffMap());
    }

    private void FixedUpdate()
    {
        pastPositions.Enqueue(transform.position);

        if (pastPositions.Count == 5)
        {
            pastPositions.Dequeue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var damageable = other.gameObject.GetComponent<Damageable>();
            damageable.UpdateHealth(-damage);

            audioSource.PlayOneShot(hitEnemySound);

            var force = isReturning ? -20 : 10;

            if (damageable.gameObject.TryGetComponent(out MeleeEnemy enemy))
            {
                enemy.Knockback(force, 0.8f);
            }
            else
            {
                damageable.gameObject.GetComponent<BasicEnemy>().Knockback(force, 0.8f);
            }
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

                    if(pastPositions.Count != 1)
                    pastPositions.Dequeue();

                    if (pastPositions.Count != 1)
                        pastPositions.Dequeue();

                    transform.rotation = startingRotation;
                    transform.Rotate(transform.right, -30);
                    transform.position = pastPositions.First();

                    audioSource.Stop();
                    audioSource.PlayOneShot(hitSound);

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
        rigidbody.angularVelocity = angularVelocity;

        while (true)
        {
            rigidbody.MovePosition(Vector3.MoveTowards(transform.position, playerTransform.position, Time.fixedDeltaTime * returningForce));

            yield return new WaitForFixedUpdate();
        }
    }
    #endregion
}
