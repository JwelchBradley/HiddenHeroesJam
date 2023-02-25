using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    #region Fields

    #endregion

    #region Functions
    public abstract void WeaponDown();

    public abstract void WeaponUp();

    public abstract void AltWeaponDown();

    public abstract void AltWeaponUp();
    #endregion
}
