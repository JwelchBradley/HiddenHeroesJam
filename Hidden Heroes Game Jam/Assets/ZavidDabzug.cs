using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZavidDabzug : MonoBehaviour
{
    public int phase;

    public Sprite[] normalSprites, greenSprites;

    SpriteRenderer spr;

    Color normalColor;
    public Color invisColor;

    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponentInChildren<SpriteRenderer>();
        normalColor = spr.color;

        if(phase == 2)
        {
            StartCoroutine(GoInvis());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GoInvis()
    {
        spr.color = normalColor;
        spr.sprite = normalSprites[1];
        yield return new WaitForSeconds(0.5f);
        spr.color = invisColor;
        spr.sprite = greenSprites[1];
        yield return new WaitForSeconds(0.5f);
        spr.color = normalColor;
        spr.sprite = normalSprites[1];
        yield return new WaitForSeconds(0.5f);
        spr.color = invisColor;
        spr.sprite = greenSprites[1];
    }
}
