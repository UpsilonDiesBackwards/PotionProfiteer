using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterTrain : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("TrainInterior");
        }
    }
}
;