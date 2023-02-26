/*****************************************************************************
// File Name :         HealthVignetteHandler.cs
// Author :            #AUTHOR#
// Contact :           #CONTACT#
// Creation Date :     #DATE#
// Company :           #COMPANY#
//
// Brief Description : Description of what the script does
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthVignetteHandler : MonoBehaviour
{
    #region Fields
    [SerializeField] private int takeDamageAlpha = 100;

    [SerializeField] private float goInTakeDamageTime = 0.1f;

    [SerializeField] private float goOutTakeDamageTime = 0.1f;

    [SerializeField] private int maxVignette = 50;

    private float lastHealth = -Mathf.Infinity;

    private Image spriteRenderer;
    private Coroutine vignetteRoutine;
    #endregion

    #region Functions
    // Start is called before the first frame update
    private void Awake()
    {
        spriteRenderer = GetComponent<Image>();
        FindObjectOfType<PlayerController>().HealthChangeEvent.AddListener(ChangeVignette);
    }
    
    private void ChangeVignette(float currentHealth, float maxHealth)
    {
        if(vignetteRoutine != null)
        {
            StopCoroutine(vignetteRoutine);
            vignetteRoutine = null;
        }

        if(lastHealth > currentHealth || lastHealth == currentHealth)
        {
            print(lastHealth);
            vignetteRoutine = StartCoroutine(HealthVignetteUpdate(currentHealth / maxHealth));
        }

        lastHealth = currentHealth;
        print(lastHealth);
        print(currentHealth);
    }

    private IEnumerator HealthVignetteUpdate(float lerp)
    {
        var t = 0.0f;
        var target = Mathf.Lerp(maxVignette, 0.0f, lerp);
        Color color = Color.white;
        color.a = 0;

        while (t < goInTakeDamageTime)
        {
            yield return new WaitForFixedUpdate();
            t += Time.fixedDeltaTime;
            color.a = Mathf.Lerp(0.0f, takeDamageAlpha, t / goInTakeDamageTime)/255;

            spriteRenderer.color = color;
        }

        t = 0.0f;

        while (t < goOutTakeDamageTime)
        {
            yield return new WaitForFixedUpdate();
            t += Time.fixedDeltaTime;
            color.a = Mathf.Lerp(takeDamageAlpha, target, t / goOutTakeDamageTime)/255;

            spriteRenderer.color = color;
        }

        vignetteRoutine = null;
    }
    #endregion
}
