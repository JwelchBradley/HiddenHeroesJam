using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerController : Damageable
{
    #region Fields
    private Weapon weapon;
    [SerializeField] private bool startWithAxe = true;

    private CinemachineImpulseSource cameraShakeSource;
    private Coroutine deathRoutine;

    private GameObject Discord;
    bool DiscordOn = false;
    #endregion

    #region Functions
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        Discord = GameObject.Find("Discord");

        if(Discord != null)
        Discord.SetActive(false);

        cameraShakeSource = GetComponent<CinemachineImpulseSource>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        var healthBar = GameObject.Find("PlayerHealthBar");

        if(healthBar != null)
        HealthChangeEvent.AddListener(healthBar.GetComponent<HealthBarHandler>().UpdateHealthBar);
    }

    private void Start()
    {
        if (startWithAxe)
        {
            GetAxe();
        }
        else
        {
            RemoveAxe();
        }
    }

    public void RemoveAxe()
    {
        GetComponentInChildren<AxeWeapon>().ShowClub(false);
    }

    public void GetAxe()
    {
        weapon = GetComponentInChildren<Weapon>();
        weapon.gameObject.GetComponent<AxeWeapon>().ShowClub(true);
    }

    // Update is called once per frame
    private void Update()
    {
        WeaponInput();
        OpenDiscord();
    }

    private void WeaponInput()
    {
        if (weapon == null || isDead) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            weapon.WeaponDown();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            weapon.AltWeaponDown();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            weapon.WeaponUp();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            weapon.AltWeaponUp();
        }
    }

    private void OpenDiscord()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Discord == null)
            {
                Discord = GameObject.Find("Discord");
            }

            if (Discord == null) return;

            Discord.SetActive(!Discord.activeSelf);
        }
    }

    public override void UpdateHealth(float healthMod)
    {
        if(healthMod < 0)
        {
            if(cameraShakeSource != null)
            cameraShakeSource.GenerateImpulse();
        }

        base.UpdateHealth(healthMod);
    }

    protected override void DestructionEvent()
    {
        if(deathRoutine == null)
        deathRoutine = StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(1.0f);

        FindObjectOfType<MenuBehavior>().LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hazards")
        {
            UpdateHealth(-Mathf.Infinity);
            //SceneManager.LoadScene(currentScene);
        }
    }
    #endregion
}
