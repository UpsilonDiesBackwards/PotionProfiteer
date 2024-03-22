using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPhaseDisplay : MonoBehaviour
{

    public Text phaseDisplayText;
    private Touch theTouch;
    private float touchTimeEnded;
    private float displayTime = 0.5f;
 
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) //if there is a finger on the screen
        {
            theTouch = Input.GetTouch(0);
            if (theTouch.phase == TouchPhase.Ended )
            {
                phaseDisplayText.text = theTouch.phase.ToString();
                touchTimeEnded = Time.time;
            }
            else if(Time.time - touchTimeEnded > displayTime)
            {
                phaseDisplayText.text = theTouch.phase.ToString();
                touchTimeEnded = Time.time;
            }
        }
        else if (Time.time - touchTimeEnded > displayTime)
        {
            phaseDisplayText.text = " ";
        }
    }
}
