using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//食材类
[System.Serializable]
public class Ingredient
{
    [System.Serializable]
    public enum IngredientType
    {
        Meat,
        Vegetable,
        Staple, //主食
        Spicy,
        Oil,
        Powder
    }

    [SerializeField]
    private string m_Name;
    [SerializeField]
    private IngredientType m_Type;
    [SerializeField]
    private int m_Amount;

    public string Name { get => m_Name; set => m_Name = value; }
    public IngredientType Type { get => m_Type; set => m_Type = value; }
    public int Amount { get => m_Amount; set => m_Amount = value; }
}
