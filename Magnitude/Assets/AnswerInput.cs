using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class AnswerInput : MonoBehaviour
{

	InputField inputField;
	private float timer;
	private int testCount;
	private float magnitudeValue;


	/// <summary>
	/// </summary>
	void Start()
	{
		testCount = PlayerPrefs.GetInt("count");
		magnitudeValue = float.Parse(CSVReader.originalDatas[testCount - 1][0]);
		inputField = GetComponent<InputField>();
		//		LogSave.logSave (CSVReader.originalDatas[2][3],CSVReader.originalDatas[2][3],CSVReader.originalDatas[2][3],"test");
		InitInputField();
		string filePath = Path.Combine(Application.dataPath, "Results/" + NameInput.subjectName);
		Directory.CreateDirectory(filePath);
	}

	void Update()
	{
		timer = Time.timeSinceLevelLoad;
		if (Input.GetKeyDown(KeyCode.B))
		{
			PlayerPrefs.SetInt("count", testCount - 1);
			SceneManager.LoadScene("TestPage");
		}
	}


	/// <summary>
	/// </summary>


	public void InputLogger()
	{

		string inputValue = inputField.text;

		//Debug.Log(inputValue);
		textSave(inputValue);
		InitInputField();

		if (PlayerPrefs.GetInt("count") >= 30)
		{
			SceneManager.LoadScene("StartPage");
		}
		else if (PlayerPrefs.GetInt("count") < 10)
		{
			SceneManager.LoadScene("Reference");
		}
		else if (PlayerPrefs.GetInt("count") % 10 == 0)
		{
			SceneManager.LoadScene("Rest");
		}
		else
		{
			SceneManager.LoadScene("TestPage");
		}
	}

	public void textSave(string txt)
	{
		StreamWriter sw = new StreamWriter("./Assets/Results/" + NameInput.subjectName + "/" + PlayerPrefs.GetInt("test") + "_result.csv", true); //true=追記 false=上書き
		var line = string.Format("{0},{1},{2}", magnitudeValue/36f , txt, timer);
		sw.WriteLine(line);
		sw.Flush();
		sw.Close();
	}


	void InitInputField()
	{

		inputField.text = "";

		inputField.ActivateInputField();
	}

}