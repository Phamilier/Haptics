using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public GameObject upperBar;
    public GameObject lowerBar;
    public VibManager vibManager;
    public int Max;
    public int Min;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scale = vibManager.error / (float)Max;
        if (scale > 0)
        {
            // Adjust the Y scale of the upper bar
            AdjustYScale(upperBar, scale);
            AdjustYScale(lowerBar, 0);
        }
        else
        {
            AdjustYScale(upperBar, 0);
            // Adjust the Y scale of the lower bar
            AdjustYScale(lowerBar, -scale); // Invert scale for lower bar
        }
    }

    void AdjustYScale(GameObject bar, float yScale)
    {
        Transform barTransform = bar.transform;

        // Get the current local scale
        Vector3 currentScale = barTransform.localScale;

        // Set the new Y scale while keeping the other axes unchanged
        barTransform.localScale = new Vector3(currentScale.x, yScale, currentScale.z);
    }
}
