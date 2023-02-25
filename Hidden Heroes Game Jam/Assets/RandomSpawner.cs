using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] spawns;
    public float minTime, maxTime;
    float timer;

    private void Start()
    {
        timer = Time.time + Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        if(Time.time > timer)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Instantiate(spawns[Random.Range(0, spawns.Length)], transform.position, transform.rotation);
        timer = Time.time + Random.Range(minTime, maxTime);
    }
}
