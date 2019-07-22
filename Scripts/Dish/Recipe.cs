using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RecipeType : uint
{
    RecipeType_Chuan, //川菜
    RecipeType_Lu, //鲁菜
    RecipeType_Yue, //粤菜
    RecipeType_Su, //苏菜
    RecipeType_Zhe, //浙菜
    RecipeType_Min, //闽菜
    RecipeType_Xiang, //湘菜
    RecipeType_Hui, //徽菜

    RecipeType_Count
}

//菜谱类
[Serializable]
public class Recipe
{
    [SerializeField]
    private RecipeType m_Type;
    [SerializeField]
    private string m_Name;
    [SerializeField]
    private List<FlavourFactor> m_Flavours;
    [SerializeField]
    private List<FlavourFactor> m_FlavourMods;

    public RecipeType Type { get => m_Type; set => m_Type = value; }
    public string Name { get => m_Name; set => m_Name = value; }
    //原始菜谱的味道因子
    public List<FlavourFactor> Flavours { get => m_Flavours; set => m_Flavours = value; }
    //针对每个顾客的偏好的额外味道因子
    public List<FlavourFactor> FlavourMods { get => m_FlavourMods; set => m_FlavourMods = value; }
}
