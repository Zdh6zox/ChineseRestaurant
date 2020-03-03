using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager
{
    private static string m_SaveFilePath;
    private static string m_CustomerTablePath;
    private static string m_SpecialCustomerTablePath;
    private static string m_CustomerTemplatePath;
    private static string m_FirstNameTablePath;
    private static string m_SecondNameTablePath;
    private static string m_RecipeListPath;
    private static string m_RecipeNamePath;
    private static string m_FlavourNamePath;
    private static string m_CookMethodPath;

	private static SaveLoadManager m_Instance;
    
	private SaveLoadManager(string dataPath,string productName)
	{
		m_SaveFilePath = dataPath + /*"/" + productName +*/ "/Save" + "/Save.json";
		m_CustomerTablePath = dataPath + /*"/" + productName +*/ "/Save" + "/Customers.json";
        m_SpecialCustomerTablePath = dataPath + /*"/" + productName +*/ "/Save" + "/SpecialCustomers.json";
        m_RecipeListPath = dataPath + /*"/" + productName +*/"/Save" + "/RecipeList.json";
        m_CustomerTemplatePath = dataPath + "/Prefabs/Character/Customer.prefab";


        //temp
        m_FirstNameTablePath = dataPath + /*"/" + productName +*/ "/Temp" + "/FirstName.json";
        m_SecondNameTablePath = dataPath + /*"/" + productName +*/ "/Temp" + "/SecondName.json";
        m_RecipeNamePath = dataPath + /*"/" + productName +*/ "/Temp" + "/RecipeName.json";
        m_FlavourNamePath = dataPath + /*"/" + productName +*/ "/Temp" + "/FlavourName.txt";
        m_CookMethodPath = dataPath + /*"/" + productName +*/ "/Temp" + "/CookMethodName.txt";
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


    #region LoadFunctions
    public string LoadCustomerDataJson()
    {
        if(File.Exists(m_CustomerTablePath))
        {
            return File.ReadAllText(m_CustomerTablePath);
        }
        return "";
    }

    public string LoadSpecialCustomerDataJson()
    {
        if(File.Exists(m_SpecialCustomerTablePath))
        {
            return File.ReadAllText(m_SpecialCustomerTablePath);
        }
        return "";
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

    public string LoadRecipeJson()
    {
        if (File.Exists(m_RecipeListPath))
        {
            return File.ReadAllText(m_RecipeListPath);
        }
        throw new System.Exception("Cannot Find Recipe List");
    }

    public string LoadRecipeName()
    {
        if (File.Exists(m_RecipeNamePath))
        {
            return File.ReadAllText(m_RecipeNamePath);
        }
        throw new System.Exception("Cannot Find Recipe Name TxT");
    }

    public string LoadFlavourName()
    {
        if (File.Exists(m_FlavourNamePath))
        {
            return File.ReadAllText(m_FlavourNamePath);
        }
        throw new System.Exception("Cannot Find Flavour Name TxT");
    }

    public string LoadCookMethodName()
    {
        if (File.Exists(m_CookMethodPath))
        {
            return File.ReadAllText(m_CookMethodPath);
        }
        throw new System.Exception("Cannot Find Cook Method Name TxT");
    }

    public GameObject LoadCustomerTemplate()
    {
        //只在Editor模式下有效
        return UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Character/Customer.prefab");
        //return Resources.Load(m_CustomerTemplatePath) as GameObject;
    }

    public GameObject LoadWordTemplate()
    {
        //只在Editor模式下有效
        return UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UIObject/Word.prefab");
    }

    #endregion

    #region SaveFunctions
    public void SaveCustomerDataJson(List<CustomerData> customerDataTable)
    {
        string customerDataTableStr = SerializeTools.ListToJson<CustomerData>(customerDataTable);
        File.WriteAllText(m_CustomerTablePath, customerDataTableStr);
    }

    public void SaveSpecialCustomerDataJson(List<CustomerData> specialCustomerDataTable)
    {
        string customerDataTableStr = SerializeTools.ListToJson<CustomerData>(specialCustomerDataTable);
        File.WriteAllText(m_SpecialCustomerTablePath, customerDataTableStr);
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

    public void SaveRecipeJson(List<Recipe> recipeList)
    {
        string recipeListStr = SerializeTools.ListToJson<Recipe>(recipeList);
        //delete previous content
        //File.Delete(m_RecipeListPath);
        File.WriteAllText(m_RecipeListPath, recipeListStr);
    }
    #endregion
}
