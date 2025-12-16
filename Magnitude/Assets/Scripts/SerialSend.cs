using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialSend : MonoBehaviour
{
    //SerialHandler.cのクラス
    public SerialHandler serialHandler;
    public VibManager vibManager;

    private float timeOut = 0.005f;
    private float timeElapsed;
    private float[] voltage = new float[8];

    void Update() //ここは0.001秒ごとに実行される
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeOut)
        {
            timeElapsed = 0.0f;
            voltage = vibManager.voltage;
            //Debug.Log(intensity.ToString());
            serialHandler.Write(voltage[0] + " " + voltage[1] + " " + voltage[2] + " " + voltage[3] + " " + voltage[4] + " " + voltage[5] + " " + voltage[6] + " " + voltage[7] + "\n");
            //Debug.Log(vibManager.voltage[0] + " " + vibManager.voltage[1] + " " + vibManager.voltage[2] + " " + vibManager.voltage[3] + " " + vibManager.voltage[4] + " " + vibManager.voltage[5] + " " + vibManager.voltage[6] + " " + vibManager.voltage[7] + " " + "\n");
        }
    }
}
