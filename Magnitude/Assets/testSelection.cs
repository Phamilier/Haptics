using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSelection : MonoBehaviour
{
    public int testType;
    public int testCount;
    public int testDirection;
    // Start is called before the first frame update
    void Start()
    {
        testCount = 0;
        PlayerPrefs.SetInt("test", testType);
        PlayerPrefs.SetInt("count", testCount);
        PlayerPrefs.SetInt("direction", testDirection);
    }

    // Update is called once per frame
}
