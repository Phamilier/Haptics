using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
	private TextAsset csvFile; // CSV file
	public static List<string[]> originalDatas = new List<string[]>(); // the list in CSV
	private int height = 0; // The current row of CSV
	bool readFlag = false;

	void Start()
	{
		Reader();
	}

	// Update is called once per frame
	void Update()
	{
	}
	void Reader()
	{
		csvFile = Resources.Load("CSV/testforce" + PlayerPrefs.GetInt("test")) as TextAsset; /* Read the file at Resouces/CSV */
		StringReader reader = new StringReader(csvFile.text);

		while (reader.Peek() > -1)
		{
			string line = reader.ReadLine();
			originalDatas.Add(line.Split(',')); // add to list
			height++; // increase the height
		}
		//Debug.Log(originalDatas[0][0]);
	}
}
