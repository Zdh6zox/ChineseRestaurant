using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Serialization<T>
{
	[SerializeField]
	List<T> target;

	public List<T> TargetList => target;

	public Serialization(List<T> target)
	{
		this.target = target;
	}
}


//用于系列化Dictionary<TKey,TValue>
public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> m_Keys;
    [SerializeField]
    private List<TValue> m_Values;

    Dictionary<TKey, TValue> m_TargetDic;
    public Dictionary<TKey, TValue> TargetDic { get => m_TargetDic; set => m_TargetDic = value; }

    public Serialization(Dictionary<TKey, TValue> target)
    {
        this.m_TargetDic = target;
    }

    public void OnBeforeSerialize()
    {
        m_Keys = new List<TKey>(m_TargetDic.Keys);
        m_Values = new List<TValue>(m_TargetDic.Values);
    }

    public void OnAfterDeserialize()
    {
        var count = Math.Min(m_Keys.Count, m_Values.Count);
        m_TargetDic = new Dictionary<TKey, TValue>(count);
        for (var i = 0; i < count; ++i)
        {
            m_TargetDic.Add(m_Keys[i], m_Values[i]);
        }
    }
}

public class SerializeTools
{
	public static string ListToJson<T>(List<T> list)
	{
		return JsonUtility.ToJson(new Serialization<T>(list));
	}

	public static List<T> ListFromJson<T>(string str)
	{
		return JsonUtility.FromJson<Serialization<T>>(str).TargetList;
	}

    public static string DicToJson<TKey, TValue>(Dictionary<TKey, TValue> dic)
    {
        return JsonUtility.ToJson(new Serialization<TKey, TValue>(dic));
    }

    public static Dictionary<TKey, TValue> DicFromJson<TKey, TValue>(string str)
    {
        return JsonUtility.FromJson<Serialization<TKey, TValue>>(str).TargetDic;
    }
}

