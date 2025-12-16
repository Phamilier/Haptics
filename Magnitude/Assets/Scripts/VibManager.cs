using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;
using System.IO;

public class VibManager : MonoBehaviour
{
    private int mode = 3;
    public string currentMode = "Rest";
    public float timeSinceStartup = 0;
    public float target;
    public float force;
    public float error;
    //private float maxError;
    public string DataReceived;
    public SerialHandler serialHandler;
    public pythonReceive pythonReceive;

    //public float[] Mic = new float[5];
    public float minimumVoltage;
    public float maximumVoltage;
    private float voltageRange;
    public float[] voltage = new float[8];

    private float timeOut = 0.001f;
    private float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
        voltageRange = maximumVoltage - minimumVoltage;
        initializeVol();
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
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeOut)
        {
            timeSinceStartup = Time.time;
        }
        
    }
}
