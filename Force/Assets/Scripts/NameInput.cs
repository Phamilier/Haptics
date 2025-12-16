using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class NameInput : MonoBehaviour {
	
	InputField inputField;
	public static string subjectName;


	/// <summary>
	/// Startメソッド
	/// InputFieldコンポーネントの取得および初期化メソッドの実行
	/// </summary>
	void Start() {
		inputField = GetComponent<InputField>();
//		LogSave.logSave (CSVReader.originalDatas[2][3],CSVReader.originalDatas[2][3],CSVReader.originalDatas[2][3],"test");
		InitInputField();
	}


	/// <summary>
	/// Log出力用メソッド
	/// 入力値を取得してLogに出力し、初期化
	/// </summary>


	public void InputLogger() {

		string inputValue = inputField.text;

//		Debug.Log(inputValue);
		//textSave (inputValue);
		subjectName = inputValue;
		InitInputField();

		SceneManager.LoadScene ("ForceGuidance");
	}

	public void textSave(string txt){
		StreamWriter sw = new StreamWriter("./" + subjectName + "_result.csv", true); //true=追記 false=上書き
		//sw.WriteLine(txt);
		sw.Flush();
		sw.Close ();
	}


	void InitInputField() {

		// 値をリセット
		inputField.text = "";

		// フォーカス
		inputField.ActivateInputField();
	}

}