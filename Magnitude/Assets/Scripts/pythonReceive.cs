using UnityEngine;
using NetMQ;
using NetMQ.Sockets;
using System.Threading;
using System;
using AsyncIO;

public class pythonReceive : MonoBehaviour
{
    private Thread zmqThread;
    private volatile bool isRunning = true;
    public float forceData;

    void Start()
    {
        zmqThread = new Thread(ReceiveData);
        zmqThread.Start();
    }

    void ReceiveData()
    {
        ForceDotNet.Force();
        using (var pullSocket = new PullSocket())
        {
            pullSocket.Connect("tcp://localhost:5555");

            while (isRunning)
            {
                bool received = pullSocket.TryReceiveFrameBytes(TimeSpan.FromMilliseconds(100), out byte[] messageBytes);
                if (received && messageBytes != null && messageBytes.Length >= sizeof(float))
                {
                    float receivedData = BitConverter.ToSingle(messageBytes, 0);
                    forceData = receivedData;

                    // Do something with the received data in Unity
                    //Debug.Log("Received data: " + receivedData);
                }
                else if (received && messageBytes != null)
                {
                    Debug.Log("Received incomplete data.");
                }
                else
                {
                    //Debug.Log("No data received.");
                }
            }
        }
        NetMQConfig.Cleanup(); // this line is needed to prevent unity freeze after one use, not sure why yet
    }

    void OnDestroy()
    {
        isRunning = false;  // Signal the thread to exit gracefully
        zmqThread.Join();
    }
}
