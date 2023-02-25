using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    public GameObject[] spawns;
    public float minTime, maxTime;
    float timer;
    public float dist;

    private void Start()
    {
        timer = Time.time + Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        if (Time.time > timer)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Vector2 circ = Random.insideUnitCircle;
        Instantiate(spawns[Random.Range(0, spawns.Length)], transform.position + new Vector3(circ.x, 0, circ.y) * dist, transform.rotation);
        timer = Time.time + Random.Range(minTime, maxTime);
    }
}
