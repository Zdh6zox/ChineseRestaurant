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

    RecipeType_Invalid
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
    private List<FlavourFactor> m_Flavours = new List<FlavourFactor>();
    [SerializeField]
    private List<FlavourFactor> m_FlavourMods = new List<FlavourFactor>();
    [SerializeField]
    private List<Ingredient> m_Ingredients = new List<Ingredient>();
    [SerializeField]
    private List<CookMethod> m_CookMethods = new List<CookMethod>();

    private static Recipe _InvalidRecipe;

    public RecipeType Type { get => m_Type; set => m_Type = value; }
    public string Name { get => m_Name; set => m_Name = value; }
    //原始菜谱的味道因子
    public List<FlavourFactor> Flavours { get => m_Flavours; set => m_Flavours = value; }
    //针对每个顾客的偏好的额外味道因子
    public List<FlavourFactor> FlavourMods { get => m_FlavourMods; set => m_FlavourMods = value; }
    //原料
    public List<Ingredient> Ingredients { get => m_Ingredients; set => m_Ingredients = value; }
    //制作方式
    public List<CookMethod> CookMethods { get => m_CookMethods; set => m_CookMethods = value; }



    public static Recipe GetInvalid()
    {
        if(_InvalidRecipe == null)
        {
            _InvalidRecipe = new Recipe();
            _InvalidRecipe.Type = RecipeType.RecipeType_Invalid;
        }

        return _InvalidRecipe;
    }
}
