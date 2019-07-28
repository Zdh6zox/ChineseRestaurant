using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TempFirstNameData
{
    //使用逗号隔开
    public string FirstNameString;
    public float FirstNameWeight;
}

[System.Serializable]
public class TempSecondNameData
{
    //使用逗号隔开
    public string SecondNameString;
    public GenderType SecondNameGender;
}


//临时用来生成NameTable的类
public class TempName : MonoBehaviour
{
    public List<TempFirstNameData> FirstNameDataList;
    public List<TempSecondNameData> SecondNameDataList;
    // Start is called before the first frame update
    void Start()
    {
        //List<FirstName> firstNameTable = ConstructFirstName();
        //SaveLoadManager.GetInstance().SaveFirstNameJson(firstNameTable);
        //List<SecondName> secondNameTable = ConstructSecondName();
        //SaveLoadManager.GetInstance().SaveSecondNameJson(secondNameTable);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    List<FirstName> ConstructFirstName()
    {
        List<FirstName> returnList = new List<FirstName>();
        foreach(TempFirstNameData data in FirstNameDataList)
        {
            string nameString = data.FirstNameString;
            string[] firstNameArr = nameString.Split(',');

            foreach(string firstName in firstNameArr)
            {
                FirstName newName = new FirstName(firstName, data.FirstNameWeight);
                returnList.Add(newName);
            }
        }

        return returnList;
    }

    List<SecondName> ConstructSecondName()
    {
        List<SecondName> returnList = new List<SecondName>();
        foreach (TempSecondNameData data in SecondNameDataList)
        {
            string nameString = data.SecondNameString;
            string[] secondNameArr = nameString.Split(',');

            foreach (string secondName in secondNameArr)
            {
                SecondName newName = new SecondName(secondName, data.SecondNameGender);
                returnList.Add(newName);
            }
        }

        return returnList;
    }
}
