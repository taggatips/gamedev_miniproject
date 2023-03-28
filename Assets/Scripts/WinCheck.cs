using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCheck : MonoBehaviour
{
    GameObject completeLevelUI;
    
    // Start is called before the first frame update
    void Start()
    {
        //completeLevelUI = GameObject.FindWithTag("LevelComplete1");
        //GameObject temp = GameObject.Find("LevelComplete1");
        //completeLevelUI = temp.GetComponent<Canvas>();
        //print(completeLevelUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collider)
    {
        print("intrigger");
        if (collider.transform.tag == "Player")
        {
            print("inif");
            //completeLevelUI.SetActive(true);
        }
    }
}
