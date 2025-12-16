using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RefStimulus : MonoBehaviour
{
    private float magnitudeValue;
    private int testCount;
    public MagnitudeTest magnitudeTest;
    // Start is called before the first frame update
    void Start()
    {
        magnitudeValue = 360;
        testCount = PlayerPrefs.GetInt("count");

    }

    // Update is called once per frame
    void Update()
    {
        float timer = Time.timeSinceLevelLoad - 2;
        float trialduration =  1f;
        magnitudeTest.SetMode(PlayerPrefs.GetInt("test"));
        if (testCount < 9999)
        {
            if (timer > 0 && timer < trialduration)
            {
                magnitudeTest.scaleNumber = magnitudeValue / (trialduration) * timer;
            }
            else if (timer > trialduration)
            {
                magnitudeTest.scaleNumber = magnitudeValue;
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
            else if (timer > 0 && timer < trialduration / 2f)
            {
                magnitudeTest.scaleNumber = 360 - magnitudeValue / (trialduration / 2f) * timer;
            }
            else if (timer > trialduration / 2f)
            {
                magnitudeTest.scaleNumber = 0;
            }
            else
            {
                magnitudeTest.scaleNumber = 0;
            }
        }

        if (timer > trialduration * (magnitudeValue / 360f) + 3)
        {
            magnitudeTest.scaleNumber = 0;
            SceneManager.LoadScene("TestPage");
        }
    }
}