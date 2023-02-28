using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 20f;
    public string levelName;
    bool SpacePressed = false;
    
    public AudioSource DayOff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Camera.main.transform.up * scrollSpeed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space))
         {
            StartCoroutine (SceneSwitcher());
            DayOff.Play();
           

        }

        if(SpacePressed == true)
        {

            FindObjectOfType<MenuBehavior>().LoadScene(levelName);
        }
    }

    IEnumerator SceneSwitcher()
    {
        yield return new WaitForSeconds(1);

        SpacePressed = true;

        Debug.Log("its working");

    }
}
