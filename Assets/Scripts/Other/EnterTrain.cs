using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterTrain : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D ChangeScene) //simplest way to do it, on triggers will be the death of me - Kaz
    {
        if (ChangeScene.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("TrainInterior");
        }
    }
}
;