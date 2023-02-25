using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    #region Fields
    [Tooltip("The amount of health it starts with and its max")]
    [SerializeField] private float maxHealth = 100;

    public UnityEvent<float, float> HealthChangeEvent = new UnityEvent<float, float>();

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
    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void UpdateHealth(float healthMod)
    {
        CurrentHealth += healthMod;

        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        if (CurrentHealth <= 0)
        {
            DestructionEvent();
        }
    }

    protected virtual void DestructionEvent()
    {
        Destroy(gameObject);
    }
    #endregion
}
