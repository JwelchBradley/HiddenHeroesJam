using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 20f;
    public string levelName;
    bool loading;
    
    public AudioSource DayOff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Camera.main.transform.up * scrollSpeed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space) && !loading)
         {
            Invoke("SwapScene", 1f);
            DayOff.Play();
            loading = true;
        }
    }

    void SwapScene()
    {
        FindObjectOfType<MenuBehavior>().LoadScene(levelName);
    }
}
