using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDamageable : Damageable
{
    #region Fields
    public UnityEvent deathEvent = new UnityEvent();
    #endregion

    #region Functions
    private void Start()
    {
        HealthChangeEvent.AddListener(GetComponentInChildren<HealthBarHandler>().UpdateHealthBar);
    }

    protected override void DestructionEvent()
    {
        deathEvent.Invoke();

        base.DestructionEvent();
    }
    #endregion
}
