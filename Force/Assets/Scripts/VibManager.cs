using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class VibManager : MonoBehaviour
{
    public int mode = 0;
    public string currentMode = "Rest";
    public float timeSinceStartup = 0;
    public float target;
    public float force;
    public float error;
    private float defaultMaxError;
    public string DataReceived;
    public SerialHandler serialHandler;
    public pythonReceive pythonReceive;

    //public float[] Mic = new float[5];
    public float minimumVoltage;
    public float maximumVoltage;
    private float voltageRange;
    public float[] voltage = new float[8];

    private float timeOut = 0.01f;
    private float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
        defaultMaxError = 100;
        voltageRange = maximumVoltage - minimumVoltage;
        initializeVol();
        if (PlayerPrefs.GetInt("test", 0) != 0)
        {
            SetMode(PlayerPrefs.GetInt("test"));
        }
        else
        {
            SetMode(0);
        }
        serialHandler.OnDataReceived += OnDataReceived;
        DataReceived = "0 0 0 0 0 0 0 0";
    }

    void OnDataReceived(string message)
    {
        var data = message.Split(
                new string[] { "\n" }, System.StringSplitOptions.None);
        DataReceived = data[0];
        //float DataToDecimal = Mathf.Abs(float.Parse(DataReceived, CultureInfo.InvariantCulture.NumberFormat));
        try
        {
            //Debug.Log(data[0]);
            //Debug.Log(Application.dataPath + "/Result.txt");

        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    public void SetMode(int x)
    {
        mode = x;
    }

    public void initializeVol()
    {
        for (int i = 0; i < voltage.Length; i++)
        {
            voltage[i] = 0;
        }
    }

    public void setAllVoltage(float v)
    {
        for (int i = 0; i < voltage.Length; i++)
        {
            voltage[i] = v;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            mode = 0;
            currentMode = "Rest";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mode = 1;
            currentMode = "Pull";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mode = 2;
            currentMode = "Pull Binary";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            mode = 3;
            currentMode = "Slider";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            mode = 4;
            currentMode = "Slider Binary";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            mode = 5;
            currentMode = "BarME";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            mode = 6;
            currentMode = "BarME Binary";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            mode = 7;
            currentMode = "BarPE";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            mode = 8;
            currentMode = "BarPE Binary";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            mode = 9;
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            mode = 99;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.SetFloat("Practice", target);
            SceneManager.LoadScene("Practice");
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("TestPage");
        }
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeOut)
        {
            force = pythonReceive.forceData;
            timeSinceStartup = Time.timeSinceLevelLoad;
            error = force - target;
            timeElapsed = 0.0f;
            //Debug.Log(force);

            if (mode == 0)
            {
                initializeVol();
            }

            if (mode == 1)
            {
                currentMode = "Pull";
                initializeVol();

                float maxError = defaultMaxError;

                if (Mathf.Abs(error) < 3)
                {
                    //Do nothing
                }
                else if (error < 0)
                {
                    if (error > -maxError)
                    {
                        voltage[1] = minimumVoltage + voltageRange / maxError * Mathf.Abs(error);
                        voltage[2] = minimumVoltage + voltageRange / maxError * Mathf.Abs(error);
                        voltage[3] = minimumVoltage + voltageRange / maxError * Mathf.Abs(error);
                    }
                    else
                    {
                        voltage[1] = maximumVoltage;
                        voltage[2] = maximumVoltage;
                        voltage[3] = maximumVoltage;
                    }
                }
                else
                {
                    if (error < maxError)
                    {
                        voltage[5] = minimumVoltage + voltageRange / maxError * Mathf.Abs(error);
                        voltage[6] = minimumVoltage + voltageRange / maxError * Mathf.Abs(error);
                        voltage[7] = minimumVoltage + voltageRange / maxError * Mathf.Abs(error);
                    }
                    else
                    {
                        voltage[5] = maximumVoltage;
                        voltage[6] = maximumVoltage;
                        voltage[7] = maximumVoltage;
                    }
                }
            }

            else if (mode == 2)
            {
                currentMode = "Pull binary";
                initializeVol();

                float maxError = target;

                float scale = (maxError / 8);
                int level = Mathf.CeilToInt(Math.Abs(error) / scale);
                if (Mathf.Abs(error) < 3)
                {
                    //Do nothing
                }
                else if (error < 0)
                {
                    if (error > -maxError)
                    {
                        voltage[1] = minimumVoltage + voltageRange / maxError * Mathf.Abs(error);
                        voltage[2] = minimumVoltage + voltageRange / maxError * Mathf.Abs(error);
                        voltage[3] = minimumVoltage + voltageRange / maxError * Mathf.Abs(error);
                    }
                    else
                    {
                        voltage[1] = maximumVoltage;
                        voltage[2] = maximumVoltage;
                        voltage[3] = maximumVoltage;
                    }
                }
                else
                {
                    if (error < maxError)
                    {
                        voltage[5] = minimumVoltage + voltageRange / maxError * Mathf.Abs(error);
                        voltage[6] = minimumVoltage + voltageRange / maxError * Mathf.Abs(error);
                        voltage[7] = minimumVoltage + voltageRange / maxError * Mathf.Abs(error);
                    }
                    else
                    {
                        voltage[5] = maximumVoltage;
                        voltage[6] = maximumVoltage;
                        voltage[7] = maximumVoltage;
                    }
                }
            }

            else if (mode == 7)
            {
                currentMode = "BarME";
                initializeVol();

                float maxError = defaultMaxError;

                float scale = (maxError / 8);
                int numberActivated = Mathf.FloorToInt(Math.Abs(error) / scale);
                float alpha = (Math.Abs(error) % scale) / scale;
                if (Math.Abs(error) <= 5)
                {
                    //Do nothing
                }
                else if (Math.Abs(error) > maxError)
                {
                    setAllVoltage(4);
                }
                else if (Math.Abs(error) < maxError && error < 0)
                {
                    voltage[0] = minimumVoltage;
                    for (int i = 0; i < numberActivated; i++)
                    {
                        voltage[(i + 1) % voltage.Length] = maximumVoltage; //Change 0, 1, 2, 3, 4, 5, 6, 7 to 1, 2, 3, 4, 5, 6, 7, 0
                    }
                    if (numberActivated < voltage.Length)
                    {
                        voltage[(numberActivated + 1) % voltage.Length] = alpha == 0 ? 0 : minimumVoltage + voltageRange * alpha;
                    }
                }
                else
                {
                    voltage[0] = minimumVoltage;
                    for (int i = 0; i < voltage.Length; i++)
                    {
                        if (i < numberActivated)
                        {
                            voltage[(voltage.Length - i - 1) % voltage.Length] = maximumVoltage;
                        }
                        voltage[(voltage.Length - numberActivated - 1) % voltage.Length] = alpha == 0 ? 0 : minimumVoltage + voltageRange * alpha;
                    }
                }

            }

            else if (mode == 8)
            {
                currentMode = "BarME Binary";
                initializeVol();

                float maxError = target;

                float scale = (maxError / 8);
                int numberActivated = Mathf.FloorToInt(Math.Abs(error) / scale);
                float alpha = (Math.Abs(error) % scale) / scale;

                if (Math.Abs(error) > maxError)
                {
                    numberActivated = voltage.Length - 1;
                }

                if (Math.Abs(error) <= 5)
                {
                    //Do nothing
                }
                else if (Math.Abs(error) < maxError && error < 0)
                {
                    voltage[0] = minimumVoltage;
                    for (int i = 0; i < numberActivated; i++)
                    {
                        voltage[(i + 1) % voltage.Length] = maximumVoltage; //Change 0, 1, 2, 3, 4, 5, 6, 7 to 1, 2, 3, 4, 5, 6, 7, 0
                    }
                    /*
                    if (numberActivated < voltage.Length)
                    {
                        voltage[(numberActivated + 1) % voltage.Length] = alpha == 0 ? 0 : minimumVoltage + voltageRange * alpha;
                    }
                    */
                }
                else
                {
                    voltage[0] = minimumVoltage;
                    for (int i = 0; i < voltage.Length; i++)
                    {
                        if (i < numberActivated)
                        {
                            voltage[(voltage.Length - i - 1) % voltage.Length] = maximumVoltage;
                        }
                        //voltage[(voltage.Length - numberActivated - 1) % voltage.Length] = alpha == 0 ? 0 : minimumVoltage + voltageRange * alpha;
                    }
                }
            }

            else if (mode == 3)
            {
                currentMode = "Slider";
                initializeVol();

                float maxError = defaultMaxError;

                float scale = (maxError / 8);
                int numberActivated = Mathf.FloorToInt(Math.Abs(error) / scale);
                float alpha = (Math.Abs(error) % scale) / scale;
                float flash = Mathf.Sin(30 * timeSinceStartup) > 0 ? 1 : 0;

                if (Math.Abs(error) > maxError)
                {
                    numberActivated = voltage.Length;
                    alpha = 0;
                }

                if (Math.Abs(error) < 3)
                {
                    //Do nothing
                }
                else if (error < 0)
                {
                    voltage[numberActivated % voltage.Length] = minimumVoltage + voltageRange * (1 - alpha);
                    voltage[(numberActivated + 1) % voltage.Length] = alpha == 0 ? 0 : minimumVoltage + voltageRange * alpha;
                }
                else
                {
                    voltage[(voltage.Length - numberActivated) % voltage.Length] = (minimumVoltage + voltageRange * (1 - alpha)) * flash;
                    if (numberActivated == voltage.Length)
                    {
                        //Do nothing
                    }
                    else if (numberActivated != 0)
                    {
                        voltage[(voltage.Length - numberActivated) % voltage.Length - 1] = (minimumVoltage + voltageRange * alpha) * flash;
                    }
                    else
                    {
                        voltage[voltage.Length - 1] = alpha == 0 ? 0 : (minimumVoltage + voltageRange * alpha) * flash;
                    }
                }

            }

            else if (mode == 4)
            {
                currentMode = "Slider Binary";
                initializeVol();

                float maxError = target;

                float scale = (maxError / 8);
                int numberActivated = Mathf.FloorToInt(Math.Abs(error) / scale);
                float alpha = (Math.Abs(error) % scale) / scale;
                float flash = Mathf.Sin(30 * timeSinceStartup) > 0 ? 1 : 0;

                if (Math.Abs(error) > maxError)
                {
                    numberActivated = voltage.Length;
                    alpha = 0;
                }

                if (Math.Abs(error) < 3)
                {
                    //Do nothing
                }
                else if (error < 0)
                {
                    voltage[numberActivated % voltage.Length] = minimumVoltage + voltageRange * (1 - alpha);
                    voltage[(numberActivated + 1) % voltage.Length] = alpha == 0 ? 0 : minimumVoltage + voltageRange * alpha;
                }
                else
                {
                    voltage[(voltage.Length - numberActivated) % voltage.Length] = (minimumVoltage + voltageRange * (1 - alpha)) * flash;
                    if (numberActivated == voltage.Length)
                    {
                        //Do nothing
                    }
                    else if (numberActivated != 0)
                    {
                        voltage[(voltage.Length - numberActivated) % voltage.Length - 1] = (minimumVoltage + voltageRange * alpha) * flash;
                    }
                    else
                    {
                        voltage[voltage.Length - 1] = alpha == 0 ? 0 : (minimumVoltage + voltageRange * alpha) * flash;
                    }
                }

            }

            else if (mode == 5)
            {
                currentMode = "BarPE";
                initializeVol();

                float maxError = defaultMaxError;

                float scale = (maxError / 8);
                int numberActivated = Mathf.FloorToInt(Math.Abs(error) / scale);
                float alpha = (Math.Abs(error) % scale) / scale;
                float flash = Mathf.Sin(30 * timeSinceStartup) > 0 ? 1 : 0;

                if (Math.Abs(error) > maxError)
                {
                    numberActivated = voltage.Length;
                    alpha = 0;
                }


                if (Math.Abs(error) < 3)
                {
                    //Do nothing
                }
                
                else if (error < 0)
                {
                    voltage[numberActivated % voltage.Length] = minimumVoltage + voltageRange * (1 - alpha);
                    voltage[(numberActivated + 1) % voltage.Length] = alpha == 0 ? 0 : minimumVoltage + voltageRange * alpha;
                    for (int i = 1; i < numberActivated; i++)
                    {
                        voltage[i % voltage.Length] = minimumVoltage + 0.5f; //Change 0, 1, 2, 3, 4, 5, 6, 7 to 1, 2, 3, 4, 5, 6, 7, 0
                    }
                }
                else
                {
                    voltage[(voltage.Length - numberActivated) % voltage.Length] = (minimumVoltage + voltageRange * (1 - alpha)) * flash;
                    if (numberActivated != 0 && numberActivated != voltage.Length)
                    {
                        voltage[(voltage.Length - numberActivated) % voltage.Length - 1] = (minimumVoltage + voltageRange * alpha) * flash;
                    }
                    else
                    {
                        voltage[voltage.Length - 1] = alpha == 0 ? 0 : (minimumVoltage + voltageRange * alpha) * flash;
                    }
                    for (int i = 1; i < numberActivated; i++)
                    {
                        //voltage[(voltage.Length - i) % voltage.Length] = maximumVoltage/2 * Mathf.Abs(Mathf.Sin(10 * timeSinceStartup)); //Change 0, 1, 2, 3, 4, 5, 6, 7 to 1, 2, 3, 4, 5, 6, 7, 0
                        voltage[(voltage.Length - i) % voltage.Length] = (minimumVoltage + 0.5f); //Change 0, 1, 2, 3, 4, 5, 6, 7 to 1, 2, 3, 4, 5, 6, 7, 0
                    }
                }

            }

            else if (mode == 6)
            {
                currentMode = "BarPE discrete";
                initializeVol();

                float maxError = target;

                float scale = (maxError / 8);
                int numberActivated = Mathf.FloorToInt(Math.Abs(error) / scale);
                float alpha = (Math.Abs(error) % scale) / scale;
                float flash = Mathf.Sin(30 * timeSinceStartup) > 0 ? 1 : 0;

                if (Math.Abs(error) > maxError)
                {
                    numberActivated = voltage.Length;
                    alpha = 0;
                }


                if (Math.Abs(error) < 3)
                {
                    //Do nothing
                }

                else if (error < 0)
                {
                    voltage[numberActivated % voltage.Length] = minimumVoltage + voltageRange * (1 - alpha);
                    voltage[(numberActivated + 1) % voltage.Length] = alpha == 0 ? 0 : minimumVoltage + voltageRange * alpha;
                    for (int i = 1; i < numberActivated; i++)
                    {
                        voltage[i % voltage.Length] = minimumVoltage + 0.5f; //Change 0, 1, 2, 3, 4, 5, 6, 7 to 1, 2, 3, 4, 5, 6, 7, 0
                    }
                }
                else
                {
                    voltage[(voltage.Length - numberActivated) % voltage.Length] = (minimumVoltage + voltageRange * (1 - alpha)) * flash;
                    if (numberActivated != 0 && numberActivated != voltage.Length)
                    {
                        voltage[(voltage.Length - numberActivated) % voltage.Length - 1] = (minimumVoltage + voltageRange * alpha) * flash;
                    }
                    else
                    {
                        voltage[voltage.Length - 1] = alpha == 0 ? 0 : (minimumVoltage + voltageRange * alpha) * flash;
                    }
                    for (int i = 1; i < numberActivated; i++)
                    {
                        //voltage[(voltage.Length - i) % voltage.Length] = maximumVoltage/2 * Mathf.Abs(Mathf.Sin(10 * timeSinceStartup)); //Change 0, 1, 2, 3, 4, 5, 6, 7 to 1, 2, 3, 4, 5, 6, 7, 0
                        voltage[(voltage.Length - i) % voltage.Length] = (minimumVoltage + 0.5f); //Change 0, 1, 2, 3, 4, 5, 6, 7 to 1, 2, 3, 4, 5, 6, 7, 0
                    }
                }

            }

            if (mode == 99)
            {
                currentMode = "Magnitude Test";
            }          
        }
        
    }
}
