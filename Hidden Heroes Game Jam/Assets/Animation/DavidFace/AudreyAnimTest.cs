using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudreyAnimTest : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    public void Heal()
    {
        anim.SetBool("HPBelowHalf", false);
    }

    public void SetHPBelowHalf()
    {
        anim.SetBool("HPBelowHalf", true);
    }

    public void TakeDamage()
    {
        anim.SetTrigger("Hurt");
    }

    public void ThrowClub()
    {
        anim.SetTrigger("Attacking");
    }
}
