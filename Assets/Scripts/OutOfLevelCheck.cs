using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class OutOfLevelCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other) {
        //TODO this is crude and needs by dynamiv at leaest the load scene.
         if (other.transform.tag == "Player") {
             SceneManager.LoadScene("TestLevel");
         }
     }

    // Update is called once per frame
    void Update()
    {
        
    }
}
