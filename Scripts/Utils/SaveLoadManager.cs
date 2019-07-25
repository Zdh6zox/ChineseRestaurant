using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private static string m_SaveFilePath;
    private static string m_CustomerTablePath;
    private static string m_FirstNameTablePath;
    private static string m_SecondNameTablePath;

	private static SaveLoadManager m_Instance;
    
    // Start is called before the first frame update
    void Start()
    {
      m_SaveFilePath = Application.dataPath + "/" + Application.productName + "/Save" + "/save.json";
      m_CustomerTablePath = Application.dataPath + "/" + Application.productName + "/Save" + "/Customers.json";
      m_FirstNameTablePath = Application.dataPath + "/" + Application.productName + "/Temp" + "/FirstName.json";
      m_SecondNameTablePath = Application.dataPath + "/" + Application.productName + "/Temp" + "/SecondName.json";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private SaveLoadManager(string dataPath,string productName)
	{
		m_SaveFilePath = dataPath + "/" + productName + "/Save" + "/save.json";
		m_CustomerTablePath = dataPath + "/" + productName + "/Save" + "/Customers.json";
		m_FirstNameTablePath = dataPath + "/" + productName + "/Temp" + "/FirstName.json";
		m_SecondNameTablePath = dataPath + "/" + productName + "/Temp" + "/SecondName.json";
	}

    public void SaveGame()
    {
        
    }

    public void LoadGame()
    {

    }

	public static SaveLoadManager GetInstance()
	{
		if (m_Instance == null)
		{
			m_Instance = new SaveLoadManager(Application.dataPath, Application.productName);
		}

		return m_Instance;
	}

	public string LoadFirstNameJson()
	{
		if(File.Exists(m_FirstNameTablePath))
		{
			return File.ReadAllText(m_FirstNameTablePath);
		}

		throw new System.Exception("Cannot Find First Name Table");
		return "";
	}

	public string LoadSecondNameJson()
	{
		if (File.Exists(m_SecondNameTablePath))
		{
			return File.ReadAllText(m_SecondNameTablePath);
		}

		throw new System.Exception("Cannot Find Second Name Table");
		return "";
	}
}
