using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOpen : MonoBehaviour
{
    public static bool isOpen = false;
    public GameObject mapScreen;

    void Update() {
        if (!isOpen && Input.GetKeyDown(KeyCode.M)) {
            OpenMap();
        }

        if (isOpen) {
            OpenMap(); 
        } else {
            CloseMap();
        }
    }

    void OpenMap() {
        mapScreen.SetActive(true);
        Time.timeScale = 1.0f;
        isOpen = true;
    }

    void CloseMap() {
        mapScreen.SetActive(false);
        Time.timeScale = 1.0f;
        isOpen = false;
    }
}
