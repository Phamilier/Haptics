using UnityEngine;
using NetMQ;
using NetMQ.Sockets;
using System.Threading;
using System;
using System.IO;
using System.Collections.Concurrent;

public class pythonReceive : MonoBehaviour
{
    private Thread zmqThread;
    private bool running;
    public float forceData;
    //private volatile float sharedForceData = 0.0f;
    //public VibManager vibManager;
    private ConcurrentQueue<float> dataQueue = new ConcurrentQueue<float>();

    private void Start()
    {
        //RequestDataStart();
        //ClearBuffer();
        StartZMQThread();
    }

    private void Update()
    {
        
    }

    private void StartZMQThread()
    {
        running = true;
        zmqThread = new Thread(ReceiveData);
        zmqThread.Start();
    }


    /*
    private void RequestDataStart()
    {
        using (var requestSocket = new RequestSocket())
        {
            requestSocket.Connect("tcp://localhost:5556"); // Different port for REQ/REP
            requestSocket.SendFrame("start");

            string message = requestSocket.ReceiveFrameString();
            if (message == "OK")
            {
                //Debug.Log("Python is ready to send data.");
            }
            else
            {
                //Debug.LogWarning("Unexpected reply from Python.");
            }
        }
    }

    private void ClearBuffer(PullSocket socket)
    {
        //Debug.Log("Clearing buffer...");
        while (socket.TryReceiveFrameBytes(TimeSpan.FromMilliseconds(1), out _))
        {
            // Continuously pull messages from the buffer and discard them
        }
        //Debug.Log("Buffer cleared.");
    }
    */


    private void ReceiveData()
    {
        //ForceDotNet.Force();
        using (var pullSocket = new PullSocket())
        {
            pullSocket.Connect("tcp://localhost:5555");
            //ClearBuffer(pullSocket);

            while (running)
            {
                //bool received = pullSocket.TryReceiveFrameBytes(TimeSpan.FromMilliseconds(5), out byte[] messageBytes);
                var message = pullSocket.ReceiveFrameBytes();
                if (message != null && message.Length == 4) // 4 bytes for a float
                {
                    DateTime currentTimestamp = DateTime.Now;

                    forceData = BitConverter.ToSingle(message, 0);
                    //dataQueue.Enqueue(sharedForceData);
                    //vibManager.force = sharedForceData;
                    //StreamWriter sw = new StreamWriter("./test.csv", true);
                    //var line = string.Format("{0},{1}", currentTimestamp.ToString("ss.fff"), forceData);
                    //sw.WriteLine(line);
                    //sw.Flush();
                    //sw.Close();
                    //Debug.Log($"Received float value: {forceData}");
                }
                else
                {
                    //Debug.LogWarning("Received invalid data.");
                }
                //Thread.Sleep(5); // 5ms sleep gives roughly 200Hz, but this doesn't guarantee exact timing
            }
        }
        NetMQConfig.Cleanup();
    }

   
    private void OnDestroy()
    {
        running = false;
        zmqThread.Join();
    }
}

/*
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
                bool received = pullSocket.TryReceiveFrameBytes(TimeSpan.FromMilliseconds(10), out byte[] messageBytes);
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
}*/
