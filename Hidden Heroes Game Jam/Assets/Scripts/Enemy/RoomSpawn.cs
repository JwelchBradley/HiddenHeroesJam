using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : MonoBehaviour
{
    [Tooltip("Enemies that will be enabled on entrance")]
    public GameObject[] spawns;
    [Tooltip("Wall that will close when you hit this trigger")]
    public GameObject realWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach(GameObject spawn in spawns)
            {
                spawn.SetActive(true);
            }

            realWall.SetActive(true);

            this.enabled = false;
        }
    }
}
