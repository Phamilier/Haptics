using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PracticeController : MonoBehaviour
{
    private float targetValue;
    private float completionTime;
    private int flag;
    private bool inside;
    public VibManager vibManager;
    public float timer;
    private float score;
    private float timeOut = 0.01f;
    private float timeElapsed;
    public pythonReceive pythonReceive;
    public Countdown countdown;
    // Start is called before the first frame update
    void Start()
    {
        inside = false;
        score = 0;
        timer = 0;
        flag = 0;
        completionTime = 9999;
        targetValue = PlayerPrefs.GetFloat("Practice");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("ForceGuidance");
        }
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeOut)
        {
            vibManager.SetMode(0);
            timer = Time.timeSinceLevelLoad;
            timeElapsed = 0.0f;
            if (timer > 3 && !countdown.complete)
            {
                vibManager.SetMode(PlayerPrefs.GetInt("test"));
                vibManager.target = targetValue;

                if (vibManager.force - targetValue > 9)
                {
                    completionTime = 9999;
                    flag = 1;
                }

                if (Mathf.Abs(vibManager.force - targetValue) < 3 && !inside)
                {
                    completionTime = timer;
                    inside = true;
                }
                else if (Mathf.Abs(vibManager.force - targetValue) > 6)
                {
                    completionTime = 9999;
                    inside = false;
                }


                if (timer - 3 > completionTime || (timer > 18))
                {
                    vibManager.SetMode(0);
                    countdown.complete = true;

                    if (timer - 3 > completionTime && flag != 1)
                    {
                        score = 15f - completionTime + 3f;
                    }
                    else
                    {
                        score = 0;
                    }

                    if (score != 0)
                    {
                        countdown.type = 1;
                    }
                    else if (flag == 1)
                    {
                        countdown.type = 3;
                    }
                    else
                    {
                        countdown.type = 2;
                    }
                }
            }
        }
    }
}
