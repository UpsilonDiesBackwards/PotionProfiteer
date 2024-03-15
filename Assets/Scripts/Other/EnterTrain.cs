using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterTrain : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D ChangeScene)
    {
        if (ChangeScene.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("TrainInterior");
        }
    }
}
;