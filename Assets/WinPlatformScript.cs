using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlatformScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        print("intrigger");
        if (collider.transform.tag == "Player")
        {
            LevelManager.instance.TriggerLoad();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
