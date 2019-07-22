using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private string m_SaveFilePath;
    private string m_CustomerTablePath;
    private string m_FirstNameTablePath;
    private string m_SecondNameTablePath;
    
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

    public void SaveGame()
    {
        
    }

    public void LoadGame()
    {

    }
}
