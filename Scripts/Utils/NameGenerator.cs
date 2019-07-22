using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstName
{
    [SerializeField]
    private string m_FirstNameStr;
    [SerializeField]
    private float m_Weight; //用这个来确定姓的出现频率，值越高频率越高
}

public class SecondName
{
    [SerializeField]
    private string m_SecondName;
    [SerializeField]
    private GenderType m_Gender;  //名的话，考虑是否是男/女/中性的名字
}

public class NameGenerator
{
    private List<FirstName> m_FirstNamesCache;
    private List<SecondName> m_SecondNameCache;

    public NameGenerator(string firstNameJson,string secondNameJson)
    {
        m_FirstNamesCache = JsonUtility.FromJson<List<FirstName>>(firstNameJson);
        m_SecondNameCache = JsonUtility.FromJson<List<SecondName>>(secondNameJson);
    }


}
