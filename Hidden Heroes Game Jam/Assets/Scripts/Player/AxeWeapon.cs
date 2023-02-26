using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWeapon : Weapon
{
    #region Fields
    [SerializeField] private float davidAttackFaceTime = 0.5f;
    [SerializeField] private Sprite bloodyClub;

    #region Melee
    [Header("Melee")]
    [SerializeField] private float holdInputRefTime = 0.25f;
    private Coroutine holdInputReference;

    [SerializeField] private AudioClip meleeSound;
    [SerializeField] private float timeBetweenSwings = 0.5f;
    [SerializeField] private float meleeRange = 1.0f;
    [SerializeField] private float meleeDamage = 1.0f;

    [SerializeField] private float knockbackForce = 100.0f;

    [SerializeField] private float meleeCameraShakeFrequency = 1.0f;
    [SerializeField] private float meleeCameraShakeDuration = 0.3f;


    private float timeOfLastSwing = -Mathf.Infinity;

    private CinemachineImpulseSource impulseSource;

    private BoxCollider meleeCollider;
    private List<Damageable> enemiesInMelee = new List<Damageable>();
    #endregion

    #region Throwing
    [Header("Thrown Axe")]
    [SerializeField] private AudioClip throwSound;
    [SerializeField] private AudioClip catchSound;
    [SerializeField] private GameObject thrownAxePrefab;
    [SerializeField] private float thrownDamage = 1.0f;

    [Space(15)]

    #region Throwing forces and rotations
    [SerializeField] private float throwForce = 100.0f;
    [SerializeField] private float knockbackDuration = 0.8f;
    [SerializeField] private float throwUpForce = 100.0f;
    [SerializeField] private float throwRotation = 15.0f;
    [SerializeField] private float returningForce = 100.0f;
    #endregion

    [Space(15)]

    #region Time Before
    [SerializeField] private float timeBeforeReturning = 1.0f;
    [SerializeField] private float timeBeforeGaruanteedReturn = 8.0f;
    #endregion

    #region Zoom
    [Header("Zoom")]
    [SerializeField] private float aimZoom = 2.0f;
    [SerializeField] private float zoomInSpeed = 2.0f;
    [SerializeField] private float zoomOutSpeed = 4.0f;

    [SerializeField] private float cameraShakeFrequency = 1.0f;

    private float currentCameraShake = 0.0f;

    private float CurrentCameraShake
    {
        get => currentCameraShake;
        set
        {
            cameraController.CameraShake = -currentCameraShake;
            currentCameraShake = value;
            cameraController.CameraShake = currentCameraShake;
        }
    }

    private float currentZoom = 1.0f;
    private Coroutine zoomRoutine;

    private PlayerCameraController cameraController;
    #endregion

    private GameObject club;
    private Animator clubAnimator;
    private Animator davidFaceAnimator;
    private AudioSource audioSource;

    private ThrownAxeBehaviour thrownAxeBehaviour;
    private bool isAiming = false;
    #endregion
    #endregion

    #region Functions
    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        audioSource = GetComponent<AudioSource>();

        club = GameObject.FindGameObjectWithTag("Club");
        club.GetComponent<SpriteRenderer>().enabled = true;
        clubAnimator = club.GetComponent<Animator>();

        meleeCollider = GetComponent<BoxCollider>();
        meleeCollider.size = new Vector3(2, 2, meleeRange);
        meleeCollider.center = new Vector3(0, 0, meleeRange / 2);
        cameraController = FindObjectOfType<PlayerCameraController>();
    }

    private void Start()
    {
        davidFaceAnimator = FindObjectOfType<DavidFace>().anim;
    }

    #region Melee
    public override void WeaponDown()
    {
        if (isAiming)
        {
            StopAiming();
        }
        else if(thrownAxeBehaviour == null)
        {
            if(Time.time > timeOfLastSwing + timeBetweenSwings)
            {
                Melee();
            }
            else
            {
                if(holdInputReference != null)
                {
                    StopCoroutine(holdInputReference);
                }

                holdInputReference = StartCoroutine(AttackInputReference());
            }
        }
    }

    private void Melee()
    {
        timeOfLastSwing = Time.time;

        if(meleeSound != null)
        audioSource.PlayOneShot(meleeSound);

        impulseSource.GenerateImpulse();
        StartCoroutine(AttackFace());
        clubAnimator.SetTrigger("melee");

        foreach (Damageable damageable in enemiesInMelee)
        {
            if(damageable != null)
            {
                damageable.UpdateHealth(-meleeDamage);
                club.GetComponent<SpriteRenderer>().sprite = bloodyClub;

                if (damageable.gameObject.TryGetComponent(out MeleeEnemy enemy))
                {
                    enemy.Knockback(knockbackForce, knockbackDuration);
                }
                else
                {
                    damageable.gameObject.GetComponent<BasicEnemy>().Knockback(knockbackForce, knockbackDuration);
                }
            }
        }
    }

    private IEnumerator AttackInputReference()
    {
        var t = 0.0f;

        while(t < holdInputRefTime)
        {
            yield return new WaitForFixedUpdate();

            t += Time.fixedDeltaTime;

            if (Time.time > timeOfLastSwing + timeBetweenSwings && thrownAxeBehaviour == null && !isAiming)
            {
                Melee();
            }
        }

        holdInputReference = null;
    }

    private IEnumerator AttackFace()
    {
        davidFaceAnimator.SetBool("Attacking", true);

        yield return new WaitForSeconds(davidAttackFaceTime);

        davidFaceAnimator.SetBool("Attacking", false);
    }

    #region Melee Finding Enemies
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemiesInMelee.Add(other.gameObject.GetComponent<Damageable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemiesInMelee.Remove(other.gameObject.GetComponent<Damageable>());
        }
    }
    #endregion
    #endregion

    public override void WeaponUp()
    {

    }

    #region Throwing Axe
    public override void AltWeaponDown()
    {
        if (thrownAxeBehaviour == null)
        {
            if(Time.timeScale != 0)
            {
                StopZoomRoutine();

                isAiming = true;
                zoomRoutine = StartCoroutine(ZoomRoutine(zoomInSpeed, 1));
            }
        }
        else
        {
            thrownAxeBehaviour.ReturnImmediately();
        }
    }

    public override void AltWeaponUp()
    {
        if (isAiming)
        {
            if(Time.timeScale == 0)
            {
                StopAiming();
            }
            else
            {
                ThrowAxe();
            }
        }
    }

    private void StopAiming()
    {
        StopZoomRoutine();
        zoomRoutine = StartCoroutine(ZoomRoutine(zoomOutSpeed, -1));
        isAiming = false;
    }

    private void ThrowAxe()
    {
        audioSource.PlayOneShot(throwSound);

        var thrownAxe = Instantiate(thrownAxePrefab, transform.position, transform.rotation);
        var axRb = thrownAxe.GetComponent<Rigidbody>();
        axRb.AddForce(transform.forward * throwForce);
        axRb.AddForce(Vector3.up * throwUpForce);
        axRb.angularVelocity = transform.right * throwRotation;

        thrownAxe.GetComponent<SpriteRenderer>().sprite = club.GetComponent<SpriteRenderer>().sprite;

        thrownAxeBehaviour = thrownAxe.GetComponent<ThrownAxeBehaviour>();
        thrownAxeBehaviour.damage = thrownDamage;
        thrownAxeBehaviour.timeBeforeReturning = timeBeforeReturning;
        thrownAxeBehaviour.returningForce = returningForce;
        thrownAxeBehaviour.timeBeforeGaruanteedReturn = timeBeforeGaruanteedReturn;

        StartCoroutine(AttackFace());
        club.SetActive(false);

        StopAiming();
    }

    public void ShowClub(bool shouldShow)
    {
        club.SetActive(shouldShow);
    }

    public void ReturnSound()
    {
        audioSource.PlayOneShot(catchSound);
    }

    #region Zoom
    private void StopZoomRoutine()
    {
        if (zoomRoutine != null)
        {
            StopCoroutine(zoomRoutine);
            zoomRoutine = null;
        }
    }

    private IEnumerator ZoomRoutine(float speed, int mod)
    {
        bool isZoomingIn = mod == 1;
        var goal = isZoomingIn ? aimZoom : 1;

        clubAnimator.SetBool("isWindingUp", isZoomingIn);

        while (currentZoom != goal)
        {
            currentZoom -= mod * speed * Time.fixedDeltaTime;
            currentZoom = Mathf.Clamp(currentZoom, aimZoom, 1);
            cameraController.Zoom = currentZoom;

            CurrentCameraShake = Mathf.Lerp(0, cameraShakeFrequency, Mathf.InverseLerp(1, aimZoom, currentZoom));

            yield return new WaitForFixedUpdate();
        }

        zoomRoutine = null;
    }
    #endregion
    #endregion
    #endregion
}
