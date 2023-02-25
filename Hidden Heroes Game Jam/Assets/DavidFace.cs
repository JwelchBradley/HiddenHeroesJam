using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DavidFace : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;
    Damageable dmg;

    private void Start()
    {
        anim = GetComponent<Animator>();
        dmg = GameObject.FindWithTag("Player").GetComponent<Damageable>();

        dmg.HealthChangeEvent.AddListener(UpdateHealthFace);
    }

    void UpdateHealthFace(float hp, float maxHp)
    {
        anim.SetTrigger("Hurt");

        if(hp/maxHp <= 0.5f)
        {
            anim.SetBool("HPBelowHalf", true);
        }
        else
        {
            anim.SetBool("HPBelowHalf", false);
        }
    }
}
