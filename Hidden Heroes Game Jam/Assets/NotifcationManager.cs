using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifcationManager : MonoBehaviour
{
    public Text notficationText;
    private static NotifcationManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            StartNotification();
        } 
           
        
          
    }


    public static void StartNotification()
    {
        instance.StartCoroutine(instance.sendNotification("New Message, Press D to open.", 5));
    }

     private IEnumerator sendNotification(string text, int time)
    {
        notficationText.text = text;
        yield return new WaitForSeconds(time);
        notficationText.text = "";
    }
}
