using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeBehaviour : MonoBehaviour
{
    public GameObject Discord;

     public int Counter = 0;

    public GameObject T1;
    public GameObject T2;
    public GameObject T3;
    public GameObject T4;
    public GameObject T5;
    public GameObject T6;
    public GameObject T7;
    public GameObject T8;
    public GameObject T9;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckCounter();
    }

    public void AddCounter()
    {
        Counter++;
    }


    public void CheckCounter()
    {
        if(Counter == 1)
        {
            T1.gameObject.SetActive(true);

        }

        else if(Counter == 2)
        {
            T1.gameObject.SetActive(false);
            T2.gameObject.SetActive(true);
        }

        else if(Counter == 3)
        {
            T3.gameObject.SetActive(true);
            T2.gameObject.SetActive(false);
        }
        else if(Counter == 4)
        {
            T4.gameObject.SetActive(true);
            T3.gameObject.SetActive(false);
        }
        else if(Counter == 5)
        {
            T5.gameObject.SetActive(true);
            T4.gameObject.SetActive(false);
        }
        else if(Counter == 6)
        {
            T6.gameObject.SetActive(true);
            T5.gameObject.SetActive(false);
        }

        else if(Counter == 7)
        {
            T7.gameObject.SetActive(true);
            T6.gameObject.SetActive(false);
        }

        else if(Counter == 8)
        {
            T8.gameObject.SetActive(true);
            T7.gameObject.SetActive(false);
        }
        else if(Counter == 9)
        {
            T9.gameObject.SetActive(true);
            T8.gameObject.SetActive(false);
        }
    }
}
