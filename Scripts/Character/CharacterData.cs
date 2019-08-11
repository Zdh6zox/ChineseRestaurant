using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GenderType : int
{
    GenderType_Unset = -1,  //未设定，用于中性姓名
    GenderType_Male = 1,   //男性
    GenderType_Female = 2  //女性
}

[Serializable]
public class CharacterData
{
    [SerializeField]
    private string m_CharacterName;
    [SerializeField]
    private string m_Description;
    [SerializeField]
    private GenderType m_Gender;
    [SerializeField]
    private Inventory m_Inventory;
    [SerializeField]
    private List<Relationship> m_Relations = new List<Relationship>(); //目前使用Name当Key,使用int来表示关系，范围为 [-10,10]
    [SerializeField]
    private int m_Money;

    public string CharacterName { get => m_CharacterName; set => m_CharacterName = value; }
    public string Description { get => m_Description; set => m_Description = value; }
    public GenderType Gender { get => m_Gender; set => m_Gender = value; }
    public Inventory Inventory { get => m_Inventory; set => m_Inventory = value; }
    public List<Relationship> Relations { get => m_Relations; set => m_Relations = value; }
    public int Money { get => m_Money; set => m_Money = value; }
}