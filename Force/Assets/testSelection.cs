using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSelection : MonoBehaviour
{
    public int testType;
    public int testCount;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        testCount = 0;
        score = 0;
        PlayerPrefs.SetInt("test", testType);
        PlayerPrefs.SetInt("count", testCount);
        PlayerPrefs.SetInt("score", score);
    }

    // Update is called once per frame
}
