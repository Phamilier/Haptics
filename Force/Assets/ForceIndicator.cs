using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ForceIndicator : MonoBehaviour
{
    public TextMeshProUGUI force;
    public TextMeshProUGUI target;
    public TextMeshProUGUI error;
    public VibManager vibManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        force.text = "Force: " + vibManager.force + "N";
        target.text = "Target: " + vibManager.target + "N";
        error.text = "Error: " + vibManager.error + "N";
    }
}
