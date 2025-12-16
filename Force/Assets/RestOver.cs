using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (PlayerPrefs.GetInt("count") < 12)
            {
                SceneManager.LoadScene("TestPage");
            }
            else
            {
                SceneManager.LoadScene("ForceGuidance");
            }
        }
     }
}
