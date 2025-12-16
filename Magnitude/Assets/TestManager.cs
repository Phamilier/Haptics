using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour
{
    private int testCount;
    private float magnitudeValue;
    public MagnitudeTest magnitudeTest;
    // Start is called before the first frame update
    void Start()
    {
        testCount = PlayerPrefs.GetInt("count");
        magnitudeValue = float.Parse(CSVReader.originalDatas[testCount][0]);
        Debug.Log(CSVReader.originalDatas[testCount][0]);
    }

    // Update is called once per frame
    void Update()
    {
        float timer = Time.timeSinceLevelLoad - 2;
        float trialduration = 1f;
        float slope = 360 / trialduration;
        magnitudeTest.SetMode(PlayerPrefs.GetInt("test"));
        if (magnitudeValue >= 0)
        {
            if (timer > 0 )//&& timer < trialduration / 2f)
            {
                magnitudeTest.scaleNumber = magnitudeValue < slope * timer ? magnitudeValue : slope * timer;
                //magnitudeTest.scaleNumber = magnitudeValue;
            }
            else
            {
                magnitudeTest.scaleNumber = 0;
            }
        }
        else
        {
            if (timer < 0)
            {
                magnitudeTest.scaleNumber = 360;
                //magnitudeTest.scaleNumber = magnitudeValue;
            }
            else if (timer > 0)
            {
                magnitudeTest.scaleNumber = -magnitudeValue > 360f - slope * timer ? -magnitudeValue : 360f - slope * timer;
            }
            /*
            else if (timer > trialduration / 2f)
            {
                magnitudeTest.scaleNumber = -magnitudeValue;
            }
            */
            else
            {
                magnitudeTest.scaleNumber = 0;
            }
        }

        if (timer > trialduration * (magnitudeValue/360f) + 3)
        {
            PlayerPrefs.SetInt("count", testCount + 1);
            SceneManager.LoadScene("AnswerPage");
        }
    }
}
