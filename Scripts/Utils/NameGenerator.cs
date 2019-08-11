using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FirstName : ISerializationCallbackReceiver
{
    [SerializeField]
    private string m_FirstNameStr;
    private float m_Weight; //用这个来确定姓的出现频率，值越高频率越高

	[SerializeField]
	private string m_WeightStr; //用于储存

	public float Weight { get { return m_Weight; } set { m_Weight = value; } }
	public string FirstNameStr { get => m_FirstNameStr; set => m_FirstNameStr = value; }

	public FirstName(string name, float weight)
	{
		m_FirstNameStr = name;
		m_Weight = weight;
	}

	public void OnBeforeSerialize()
	{
		m_WeightStr = string.Format("{0:0.00}", m_Weight);
	}

	public void OnAfterDeserialize()
	{
		m_Weight = float.Parse(m_WeightStr);
	}
}

[System.Serializable]
public class SecondName
{
    [SerializeField]
    private string m_SecondName;
    [SerializeField]
    private GenderType m_Gender;  //名的话，考虑是否是男/女/中性的名字

	public GenderType Gender { get { return m_Gender; } set { m_Gender = value; } }
	public string NameStr { get { return m_SecondName; } set { m_SecondName = value; } }

    public SecondName(string name, GenderType gender)
    {
        m_SecondName = name;
        m_Gender = gender;
    }
}

public class NameGenerator
{
    private List<FirstName> m_FirstNamesCache;
    private List<SecondName> m_SecondNameCache;

	private static NameGenerator m_Instance;
    private List<string> m_GeneratedNameList = new List<string>();

    private NameGenerator(string firstNameJson,string secondNameJson)
    {
        m_FirstNamesCache = SerializeTools.ListFromJson<FirstName>(firstNameJson);
		if (m_FirstNamesCache.Count == 0)
			throw new System.Exception("Empty first name table");
		m_SecondNameCache = SerializeTools.ListFromJson<SecondName>(secondNameJson);
		if (m_SecondNameCache.Count == 0)
			throw new System.Exception("Empty second name table");
	}

	public string GenerateName(GenderType type)
	{
		float randomWeight = Random.Range(0.0f, 1.0f);
		List<FirstName> qualifiedFNList = m_FirstNamesCache.FindAll(n => n.Weight >= randomWeight);
		List<FirstName> unqualifiedFNList = m_FirstNamesCache.FindAll(n => n.Weight < randomWeight);
		string finalName = "";

		List<SecondName> qualifiedSNList = m_SecondNameCache.FindAll(n => n.Gender == type || n.Gender == GenderType.GenderType_Unset);		
		while(qualifiedFNList.Count > 0)
		{
			int randomFNIndex = Random.Range(0, qualifiedFNList.Count);
			string firstName = qualifiedFNList[randomFNIndex].FirstNameStr;
			qualifiedFNList.RemoveAt(randomFNIndex);
			List<SecondName> tempSNList = new List<SecondName>(qualifiedSNList.ToArray());
			while (tempSNList.Count > 0)
			{
				int randomSNIndex = Random.Range(0, tempSNList.Count);
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
		while(unqualifiedFNList.Count > 0)
		{
			List<SecondName> tempSNList = new List<SecondName>(qualifiedSNList.ToArray());
			int randomFNIndex = Random.Range(0, unqualifiedFNList.Count);
			string firstName = unqualifiedFNList[randomFNIndex].FirstNameStr;
			while (tempSNList.Count > 0)
			{
				int randomSNIndex = Random.Range(0, tempSNList.Count);
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

		throw new System.Exception("Cannot generate name anymore!");
	}

    public static NameGenerator GetInstance()
    {
        if (m_Instance == null)
        {
            m_Instance = new NameGenerator(SaveLoadManager.GetInstance().LoadFirstNameJson(), SaveLoadManager.GetInstance().LoadSecondNameJson());
        }

        return m_Instance;
    }
}
