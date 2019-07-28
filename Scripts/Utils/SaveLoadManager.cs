using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager
{
    private static string m_SaveFilePath;
    private static string m_CustomerTablePath;
    private static string m_FirstNameTablePath;
    private static string m_SecondNameTablePath;

	private static SaveLoadManager m_Instance;
    

	private SaveLoadManager(string dataPath,string productName)
	{
		m_SaveFilePath = dataPath + /*"/" + productName +*/ "/Save" + "/save.json";
		m_CustomerTablePath = dataPath + /*"/" + productName +*/ "/Save" + "/Customers.json";
		m_FirstNameTablePath = dataPath + /*"/" + productName +*/ "/Temp" + "/FirstName.json";
		m_SecondNameTablePath = dataPath + /*"/" + productName +*/ "/Temp" + "/SecondName.json";
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

    public string LoadCustomerDataJson()
    {
        if(File.Exists(m_CustomerTablePath))
        {
            return File.ReadAllText(m_CustomerTablePath);
        }

        throw new System.Exception("Cannot Find Customer Data Table");
    }

	public string LoadFirstNameJson()
	{
		if(File.Exists(m_FirstNameTablePath))
		{
			return File.ReadAllText(m_FirstNameTablePath);
		}

		throw new System.Exception("Cannot Find First Name Table");
	}

	public string LoadSecondNameJson()
	{
		if (File.Exists(m_SecondNameTablePath))
		{
			return File.ReadAllText(m_SecondNameTablePath);
		}

		throw new System.Exception("Cannot Find Second Name Table");
	}

    public void SaveCustomerDataJson(List<CustomerData> customerDataTable)
    {
        string customerDataTableStr = SerializeTools.ListToJson<CustomerData>(customerDataTable);
        File.WriteAllText(m_SecondNameTablePath, customerDataTableStr);
    }

    public void SaveFirstNameJson(List<FirstName> firstNameTable)
	{
		string firstNameTableStr = SerializeTools.ListToJson<FirstName>(firstNameTable);
		File.WriteAllText(m_FirstNameTablePath, firstNameTableStr);
	}

    public void SaveSecondNameJson(List<SecondName> secondNameTable)
    {
        string secondNameTableStr = SerializeTools.ListToJson<SecondName>(secondNameTable);
        File.WriteAllText(m_SecondNameTablePath, secondNameTableStr);
    }
}
