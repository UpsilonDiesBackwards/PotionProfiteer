using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{
    public bool dontDestroyOnLoad = false;
    
    void Awake()
    {
        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
        if (GameObject.Find("Startup") != null && GameObject.Find("Startup").tag == "Startup")
        {
            Destroy(gameObject);
        }
    }
}
