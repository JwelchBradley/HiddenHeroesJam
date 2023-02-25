using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWeapon : Weapon
{
    #region Fields
    [SerializeField] private float timeBetweenSwings = 0.5f;
    [SerializeField] private float throwForce = 100.0f;

    [SerializeField] private float meleeRange = 1.0f;
    [SerializeField] private GameObject thrownAxePrefab;

    [SerializeField] private float meleeDamage = 1.0f;
    [SerializeField] private float thrownDamage = 1.0f;

    private BoxCollider meleeCollider;

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
        cameraController = FindObjectOfType<PlayerCameraController>();
    }

    public override void WeaponDown()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, meleeCollider.bounds.extents/2);

        foreach(Collider col in colliders)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                col.gameObject.GetComponent<Damageable>().UpdateHealth(-meleeDamage);
            }
        }
    }

    public override void WeaponUp()
    {

    }

    public override void AltWeaponDown()
    {
        StopZoomRoutine();

        print("Alt down");
        zoomRoutine = StartCoroutine(ZoomRoutine(zoomInSpeed, 1));
    }



    public override void AltWeaponUp()
    {
        StopZoomRoutine();

        zoomRoutine = StartCoroutine(ZoomRoutine(zoomOutSpeed, -1));
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
    #endregion
}
