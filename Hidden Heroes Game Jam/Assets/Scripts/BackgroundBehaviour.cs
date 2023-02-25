using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }


    public float scrollSpeed = 3;

    public const float ScrollWidth = 22; // background width/pixels per unit

    // Update is called once per frame
    void Update()
    {
        // Getting the current background position
        Vector3 pos = transform.position;

        // Moving the object to the left
        pos.z -= scrollSpeed * Time.deltaTime;

        // Check if the object is completely offscreen
        if (transform.position.x < -ScrollWidth)
        {
            Offscreen(ref pos);
        }

        // Updating the position to a new place
        transform.position = pos;
    }

    protected virtual void Offscreen(ref Vector3 pos)
    {
        pos.z += 2 * ScrollWidth;
    }
}
