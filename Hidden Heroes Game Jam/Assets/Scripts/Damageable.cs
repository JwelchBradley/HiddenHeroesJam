using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    #region Fields
    [Tooltip("The amount of health it starts with and its max")]
    [SerializeField] private float maxHealth = 100;

    [Tooltip("The object to spawn on death")]
    [SerializeField] public GameObject corpse;

    public AudioClip[] hurtSounds;
    AudioSource audioSource;

    public UnityEvent<float, float> HealthChangeEvent = new UnityEvent<float, float>();

    [HideInInspector] public bool isDead = false;

    private float currentHealth = 0;

    protected float CurrentHealth
    { 
        get => currentHealth; 
        
        set
        {
            currentHealth = value;
            HealthChangeEvent.Invoke(currentHealth, maxHealth);
        }
    }
    #endregion

    #region Functions
    protected virtual void Awake()
    {
        CurrentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void UpdateHealth(float healthMod)
    {
        if(currentHealth != 0 || healthMod > 0)
        {
            CurrentHealth += healthMod;

            if(healthMod < 0)
            {
                if(hurtSounds.Length > 0)
                {
                    audioSource.PlayOneShot(hurtSounds[Random.Range(0, hurtSounds.Length)]);
                }
            }

            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

            if (CurrentHealth <= 0)
            {
                isDead = true;
                DestructionEvent();
            }
        }
    }

    protected virtual void DestructionEvent()
    {
        if (corpse)
            Instantiate(corpse, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    #endregion
}
