using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTag : MonoBehaviour
{
    public Image image;
    
    // Update is called once per frame
    void Update()
    {
        if (image != null)
        {
            gameObject.tag = "Resource";
            Debug.Log("Added Tag");
        }
    }
}
