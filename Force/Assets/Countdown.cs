using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Countdown : MonoBehaviour
{

    public TextMeshProUGUI timer;
    public bool complete;
    public int type;
    //public TestManager testManager;
    // Start is called before the first frame update

    void Start()
    {
        complete = false;
        type = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad < 3)
        {
            timer.text = "" + Mathf.CeilToInt(3f - Time.timeSinceLevelLoad);
        }
        else if (complete)
        {
            if (type == 1)
            {
                timer.text = "Success! :)";
                timer.fontSize = 100;
            }
            else if (type == 2)
            {
                timer.text = "Faliure, overtime. :(";
                timer.fontSize = 100;
            }
            else if (type == 3)
            {
                timer.text = "Faliure, exceed the target. :(";
                timer.fontSize = 100;
            }
            else
            {
                timer.text = "Start!";
            }
        }
        else
        {
            timer.text = "Start!";
        }
    }
}
