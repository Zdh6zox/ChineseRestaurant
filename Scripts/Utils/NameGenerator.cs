using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstName
{
    [SerializeField]
    private string m_FirstNameStr;
    [SerializeField]
    private float m_Weight; //用这个来确定姓的出现频率，值越高频率越高

	public float Weight { get { return m_Weight; } set { m_Weight = value; } }
	public string NameStr { get { return m_FirstNameStr; } set { m_FirstNameStr = value; } }
}

public class SecondName
{
    [SerializeField]
    private string m_SecondName;
    [SerializeField]
    private GenderType m_Gender;  //名的话，考虑是否是男/女/中性的名字

	public GenderType Gender { get { return m_Gender; } set { m_Gender = value; } }
	public string NameStr { get { return m_SecondName; } set { m_SecondName = value; } }
}

public class NameGenerator
{
    private List<FirstName> m_FirstNamesCache;
    private List<SecondName> m_SecondNameCache;

	private static NameGenerator m_Instance;
    private List<string> m_GeneratedNameList = new List<string>();

    private NameGenerator(string firstNameJson,string secondNameJson)
    {
        m_FirstNamesCache = JsonUtility.FromJson<List<FirstName>>(firstNameJson);
		if (m_FirstNamesCache.Count == 0)
			throw new System.Exception("Empty first name table");
		m_SecondNameCache = JsonUtility.FromJson<List<SecondName>>(secondNameJson);
		if (m_SecondNameCache.Count == 0)
			throw new System.Exception("Empty second name table");
	}

	public string GenerateName(GenderType type)
	{
		if(m_Instance == null)
		{
			m_Instance = new NameGenerator(SaveLoadManager.GetInstance().LoadFirstNameJson(), SaveLoadManager.GetInstance().LoadSecondNameJson());
		}

		float randomWeight = Random.Range(0, 1);
		List<FirstName> qualifiedFNList = m_FirstNamesCache.FindAll(n => n.Weight >= randomWeight);
		List<FirstName> unqualifiedFNList = m_FirstNamesCache.FindAll(n => n.Weight < randomWeight);
		string finalName = "";

		List<SecondName> qualifiedSNList = m_SecondNameCache.FindAll(n => n.Gender == type || n.Gender == GenderType.GenderType_Unset);
		List<SecondName> tempSNList = new List<SecondName>(qualifiedSNList.ToArray());
		while(qualifiedFNList.Count > 0)
		{
			int randomFNIndex = Random.Range(0, qualifiedFNList.Count);
			string firstName = qualifiedFNList[randomFNIndex].NameStr;
			qualifiedFNList.RemoveAt(randomFNIndex);
			while(tempSNList.Count > 0)
			{
				int randomSNIndex = Random.Range(0, qualifiedSNList.Count);
				string secondName = tempSNList[randomSNIndex].NameStr;
				tempSNList.RemoveAt(randomSNIndex);
				finalName = firstName + secondName;
				if (m_GeneratedNameList.Exists(n => n == finalName))
					continue;
				else
				{
					m_GeneratedNameList.Add(firstName);
					return finalName;
				}
			}
		}

		//cannot generate name from qualified first name, do it in unqualified list

		throw new System.Exception("Cannot generate name anymore!");
		return "";
	}

    public List<string> GenerateNames(int number)
    {
		for(int i =0;i<number; ++i)
		{
			float randomWeight = Random.Range(0, 1);
			List<FirstName> qualifiedList = m_FirstNamesCache.FindAll(n => n.Weight >= randomWeight);
			int randomIndex = Random.Range(0, qualifiedList.Count - 1);
			string firstName = qualifiedList[randomIndex].NameStr;
		}
		return m_GeneratedNameList;
    }
}
