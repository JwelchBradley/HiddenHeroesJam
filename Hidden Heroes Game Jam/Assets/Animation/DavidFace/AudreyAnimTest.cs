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

    public void StartAttack()
    {
        anim.SetBool("Attacking", true);
    }

    public void StopAttack()
    {
        anim.SetBool("Attacking", false);
    }
}
