using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class TestManager : MonoBehaviour
{
    private int testCount;
    private float targetValue;
    private float completionTime;
    private int flag;
    private bool inside;
    private float score;
    private float currentScore;
    public VibManager vibManager;
    public float timer;
    private float timeOut = 0.01f;
    private float timeElapsed;
    public pythonReceive pythonReceive;
    public Countdown countdown;
    // Start is called before the first frame update
    void Start()
    {
        inside = false;
        timer = 0;
        score = 0;
        currentScore = PlayerPrefs.GetFloat("score");
        flag = 0;
        completionTime = 9999;
        testCount = PlayerPrefs.GetInt("count");
        targetValue = float.Parse(CSVReader.originalDatas[testCount][0]);
        string filePath = Path.Combine(Application.dataPath, "Results/" + NameInput.subjectName);
        Directory.CreateDirectory(filePath);
        Debug.Log(CSVReader.originalDatas[testCount][0]);
    }

    // Update is called once per frame
    void Update()
    {
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

                var line = string.Format("{0},{1},{2},{3}", timer - 3, vibManager.force, targetValue, completionTime - 3);
                StreamWriter sw = new StreamWriter("./Assets/Results/" + NameInput.subjectName + "/" + "result_" + PlayerPrefs.GetInt("test") + "_" + targetValue + "_" + (testCount + 1) + ".csv", true);
                sw.WriteLine(line);
                sw.Flush();
                sw.Close();

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


                    PlayerPrefs.SetFloat("score", currentScore + score);
                    PlayerPrefs.SetInt("count", testCount + 1);
                    StreamWriter sw2 = new StreamWriter("./Assets/Results/" + NameInput.subjectName + "/" + "result_score_" + PlayerPrefs.GetInt("test") + ".csv", true);
                    var line2 = string.Format("{0},{1},{2},{3},{4}", currentScore + score, score, flag, completionTime - 3, targetValue);
                    //sw2.WriteLine(NameInput.subjectName);
                    sw2.WriteLine(line2);
                    sw2.Flush();
                    sw2.Close();
                    SceneManager.LoadScene("Rest");
                }
            }       
        }
    }
}
