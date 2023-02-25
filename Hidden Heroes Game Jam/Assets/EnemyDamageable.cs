using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageable : Damageable
{
    #region Fields

    #endregion

    #region Functions
    private void Start()
    {
        HealthChangeEvent.AddListener(GetComponentInChildren<HealthBarHandler>().UpdateHealthBar);
    }
    #endregion
}
