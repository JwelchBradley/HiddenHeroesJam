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

    public Material greenMat;

    public GameObject cruncher;

    Collider col;
    Rigidbody rb;
    BasicEnemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        enemy = GetComponent<BasicEnemy>();

        spr = GetComponentInChildren<SpriteRenderer>();
        normalColor = spr.color;

        enemy.enabled = false;
        if(phase == 1)
        {
            StartCoroutine(Intro());
        }
        else if(phase == 2)
        {
            StartCoroutine(GoToPosition(new Vector3(30, -5, -97), 1f));
            StartCoroutine(GoInvis());
        }
        else if(phase == 3)
        {
            StartCoroutine(GoToPosition(new Vector3(30, -5, -97), 1f));
            StartCoroutine(InvisRoom());
        }
        else if(phase == 4)
        {
            StartCoroutine(GoToPosition(new Vector3(30, -5, -97), 1f));
            StartCoroutine(FinalPhase());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Intro()
    {
        GoInvinc();
        yield return new WaitForSeconds(1f);
        StartCoroutine(GoToPosition(new Vector3(30, -5, -97), 1f));
        yield return new WaitForSeconds(1f);
        enemy.enabled = true;
        NotInvinc();
    }

    IEnumerator GoInvis()
    {


        GoInvinc();
        yield return new WaitForSeconds(2f);

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
        yield return new WaitForSeconds(0.5f);
        spr.color = normalColor;
        spr.sprite = normalSprites[1];
        yield return new WaitForSeconds(0.5f);
        spr.color = invisColor;
        spr.sprite = greenSprites[1];

        yield return new WaitForSeconds(1f);

        enemy.enabled = true;
        NotInvinc();
    }

    IEnumerator InvisRoom()
    {
        GoInvinc();
        yield return new WaitForSeconds(2f);

        foreach(GameObject mesh in GameObject.FindGameObjectsWithTag("ZavidRoom"))
        {
            MeshRenderer mr = mesh.GetComponent<MeshRenderer>();
            if(mr)
                mr.material = greenMat;
        }

        yield return new WaitForSeconds(1f);
        NotInvinc();
        enemy.enabled = true;
    }

    IEnumerator FinalPhase()
    {
        GoInvinc();
        yield return new WaitForSeconds(1f);
        InvokeRepeating("SpawnCruncher", 0f, 2.5f);
        NotInvinc();
        enemy.enabled = true;
    }

    void GoInvinc()
    {
        col.enabled = false;
        rb.useGravity = false;
    }

    void NotInvinc()
    {
        col.enabled = true;
        rb.useGravity = true;
    }

    IEnumerator GoToPosition(Vector3 pos, float time)
    {
        rb.velocity = Vector3.zero;
        Vector3 startPos = transform.position;
        float timer = 0;
        while(timer < time)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, pos, timer);
            yield return new WaitForEndOfFrame();
        }
        transform.position = pos;
    }

    void SpawnCruncher()
    {
        Instantiate(cruncher, transform.position, transform.rotation);
    }
}
