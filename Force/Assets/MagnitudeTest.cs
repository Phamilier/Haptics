using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;
using System.IO;
using UnityEngine.SceneManagement;


public class MagnitudeTest : MonoBehaviour
{
    private int mode = 0;
    public string currentMode = "Rest";
    public VibManager vibManager;
    public float scaleNumber = 0;
    public float speed = 0.1f;
    private float maxScale = 360f;
    public float alpha;
    public float angle;
    private float minimumVoltage;
    private float maximumVoltage;
    private float voltageRange;

    // Start is called before the first frame update
    void Start()
    {
        int testNumber = PlayerPrefs.GetInt("test");
        mode = testNumber;
        scaleNumber = 0;
        vibManager.initializeVol();
        vibManager.SetMode(99); //Set mode to Magnitude test
        minimumVoltage = vibManager.minimumVoltage;
        maximumVoltage = vibManager.maximumVoltage;
        voltageRange = maximumVoltage - minimumVoltage;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            vibManager.initializeVol();
            mode = 0;
            currentMode = "Rest";
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            mode = 1;
            currentMode = "Magnitude";
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            mode = 2;
            currentMode = "Bar";
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            mode = 3;
            currentMode = "Slider";
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            mode = 4;
            currentMode = "Undefined";
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("TestPage");
        }


        float translation = Input.GetAxis("Vertical") * speed;
        if (scaleNumber + translation > maxScale)
        {
            scaleNumber = maxScale;
        }
        else if (scaleNumber + translation < 0)
        {
            scaleNumber = 0;
        }
        else 
        {
            scaleNumber += translation;
        }

        angle = scaleNumber % 45f;
        int motorNumber = Mathf.FloorToInt(scaleNumber / 45f);
        alpha = angle / 45f;
        if (mode == 0)
        {
            currentMode = "Rest";
        }

        if (mode == 1)
        {
            currentMode = "Magnitude";
            vibManager.initializeVol();
            vibManager.voltage[0] = minimumVoltage + voltageRange * (scaleNumber / 360f);
            vibManager.voltage[1] = minimumVoltage + voltageRange * (scaleNumber / 360f);
            vibManager.voltage[7] = minimumVoltage + voltageRange * (scaleNumber / 360f);
        }

        if (mode == 2)
        {
            currentMode = "Bar";
            vibManager.initializeVol();
            vibManager.voltage[0] = minimumVoltage;
            if (scaleNumber < maxScale)
            {
                if (motorNumber >= vibManager.voltage.Length)
                {
                    motorNumber = vibManager.voltage.Length;
                }
                for (int i = 0; i < motorNumber; i++)
                {
                    vibManager.voltage[(i + 1) % vibManager.voltage.Length] = maximumVoltage; //Change 0, 1, 2, 3, 4, 5, 6, 7 to 1, 2, 3, 4, 5, 6, 7, 0
                }
                //vibManager.voltage[motorNumber] = minimumVoltage + voltageRange * Mathf.Cos(2 * angle / 180 * Mathf.PI);
                //vibManager.voltage[(motorNumber + 1) % 8] = minimumVoltage + voltageRange * Mathf.Cos(Mathf.PI / 2 - 2 * angle / 180 * Mathf.PI);
                if (angle > 1 && motorNumber < vibManager.voltage.Length)
                {
                    vibManager.voltage[(motorNumber + 1) % vibManager.voltage.Length] = minimumVoltage + voltageRange * alpha;
                }
            }
            else
            {
                for (int i = 0; i < vibManager.voltage.Length; i++)
                {
                    vibManager.voltage[i] = maximumVoltage;
                }    
            }
        }

        if (mode == 3)
        {
            currentMode = "Slider";
            vibManager.initializeVol();
            if (scaleNumber < maxScale)
            {
                //vibManager.voltage[motorNumber] = minimumVoltage + voltageRange * Mathf.Cos(2 * angle / 180 * Mathf.PI);
                //vibManager.voltage[(motorNumber + 1) % 8] = minimumVoltage + voltageRange * Mathf.Cos(Mathf.PI / 2 - 2 * angle / 180 * Mathf.PI);
                vibManager.voltage[motorNumber] = minimumVoltage + voltageRange * (1 - alpha);
                if (angle > 1)
                {
                    vibManager.voltage[(motorNumber + 1) % 8] = minimumVoltage + voltageRange * alpha;
                }
            }
            else
            {
                vibManager.voltage[0] = maximumVoltage;
            }
        }

        /*
        if (mode == 3)
        {
            vibManager.initializeVol();
            if (scaleNumber < maxScale)
            {
                float correctedScaleNumber = scaleNumber + 22.5f;
                angle = correctedScaleNumber % 45f;
                motorNumber = Mathf.FloorToInt(correctedScaleNumber / 45f);
                alpha = angle / 45f;
                if (motorNumber == 0)
                {
                    vibManager.voltage[0] = minimumVoltage + voltageRange * (alpha - 0.5f);
                }
                else
                {
                    float alphaZero = correctedScaleNumber > 360f ? 0.5f + (correctedScaleNumber % 360f) / 45f : 0.5f;
                    vibManager.voltage[0] = minimumVoltage + voltageRange * alphaZero;
                    if (motorNumber >= vibManager.voltage.Length)
                    {
                        motorNumber = vibManager.voltage.Length;
                    }
                    for (int i = 1; i < motorNumber; i++)
                    {
                        vibManager.voltage[i] = maximumVoltage;
                    }
                    //vibManager.voltage[motorNumber] = minimumVoltage + voltageRange * Mathf.Cos(2 * angle / 180 * Mathf.PI);
                    //vibManager.voltage[(motorNumber + 1) % 8] = minimumVoltage + voltageRange * Mathf.Cos(Mathf.PI / 2 - 2 * angle / 180 * Mathf.PI);
                    if (angle > 1 && motorNumber < vibManager.voltage.Length)
                    {
                        vibManager.voltage[motorNumber] = minimumVoltage + voltageRange * alpha;
                    }
                }
            }
            else
            {
                for (int i = 0; i < vibManager.voltage.Length; i++)
                {
                    vibManager.voltage[i] = maximumVoltage;
                }
            }
        }
        */

    }


}
