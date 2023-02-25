using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWeapon : Weapon
{
    #region Fields
    [SerializeField] private float timeBetweenSwings = 0.5f;
    [SerializeField] private float throwForce = 100.0f;
    [SerializeField] private float throwRotation = 15.0f;

    [SerializeField] private float meleeRange = 1.0f;
    [SerializeField] private GameObject thrownAxePrefab;

    [SerializeField] private float meleeDamage = 1.0f;
    [SerializeField] private float thrownDamage = 1.0f;
    [SerializeField] private float timeBeforeReturning = 1.0f;
    [SerializeField] private float returningForce = 100.0f;


    [HideInInspector] public bool hasThrown = false;
    private bool isAiming = false;

    private BoxCollider meleeCollider;

    private List<Damageable> enemiesInMelee = new List<Damageable>();

    #region Zoom
    [Header("Zoom")]
    [SerializeField] private float aimZoom = 2.0f;

    [SerializeField] private float zoomInSpeed = 2.0f;
    [SerializeField] private float zoomOutSpeed = 4.0f;
    private float currentZoom = 1.0f;
    private Coroutine zoomRoutine;

    private PlayerCameraController cameraController;
    #endregion
    #endregion

    #region Functions
    private void Awake()
    {
        meleeCollider = GetComponent<BoxCollider>();
        meleeCollider.size = new Vector3(2, 2, meleeRange);
        meleeCollider.center = new Vector3(0, 0, meleeRange / 2);
        cameraController = FindObjectOfType<PlayerCameraController>();
    }

    public override void WeaponDown()
    {
        if(!hasThrown)
        {
            foreach (Damageable damageable in enemiesInMelee)
            {
                damageable.UpdateHealth(-meleeDamage);
            }
        }
    }

    public override void WeaponUp()
    {

    }

    public override void AltWeaponDown()
    {
        if (!hasThrown)
        {
            StopZoomRoutine();

            isAiming = true;
            print("Alt down");
            zoomRoutine = StartCoroutine(ZoomRoutine(zoomInSpeed, 1));
        }
    }

    public override void AltWeaponUp()
    {
        if (isAiming)
        {
            var thrownAx = Instantiate(thrownAxePrefab, transform.position, transform.rotation);
            var axRb = thrownAx.GetComponent<Rigidbody>();
            axRb.AddForce(transform.forward * throwForce);

            axRb.angularVelocity = transform.right * throwRotation;

            var thownAxe = thrownAx.GetComponent<ThrownAxeBehaviour>();
            thownAxe.damage = thrownDamage;
            thownAxe.timeBeforeReturning = timeBeforeReturning;
            thownAxe.returningForce = returningForce;


            hasThrown = true;
            isAiming = false;

            StopZoomRoutine();

            zoomRoutine = StartCoroutine(ZoomRoutine(zoomOutSpeed, -1));
        }
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
        var goal = mod == 1 ? aimZoom : 1;

        while (currentZoom != goal)
        {
            currentZoom -= mod * speed * Time.fixedDeltaTime;
            currentZoom = Mathf.Clamp(currentZoom, aimZoom, 1);
            print(currentZoom);
            cameraController.Zoom = currentZoom;
            yield return new WaitForFixedUpdate();
        }

        zoomRoutine = null;
    }
    #endregion

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
}
