using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifcationManager : MonoBehaviour
{
    

    public GameObject DiscordHUD;
    public GameObject Manager;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
          
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.enabled)
        {
            if (other.gameObject.tag == "Player")
            {
                Manager.gameObject.GetComponent<NarrativeBehaviour>().AddCounter();
                DiscordHUD.gameObject.SetActive(true);
            }
        }

        this.enabled = false;
    }

}
