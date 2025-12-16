using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorRender : MonoBehaviour
{
    private float[] colorIndex;
    public GameObject[] motorIndicator;
    public VibManager vibManager;
    private Image[] image;
    // Start is called before the first frame update
    void Start()
    {
        image = new Image[motorIndicator.Length]; // Initialize the image array
        colorIndex = new float[motorIndicator.Length];
        for (int i = 0; i < motorIndicator.Length; i++)
        {
            image[i] = motorIndicator[i].GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < motorIndicator.Length; i++)
        {
            colorIndex[i] = vibManager.voltage[i] > 1f ? (vibManager.voltage[i] - 1f)/3f : 0;
            Color targetColor = new Color(1, 0, 0, colorIndex[i]);
            image[i].color = targetColor;
        }
    }
}
