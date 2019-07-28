using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerType : uint
{
    CustomerType_Beggar, //乞丐
    CustomerType_Peasant, //农民
    CustomerType_Citizen, //市民
    CustomerType_Councillor, //员外
    CustomerType_Swordman, //侠客
    CustomerType_Escort, //镖师
    CustomerType_Merchant, //商人
    CustomerType_Hooligan, //流氓
    CustomerType_Mafia,   //地头蛇

    //新的Type加在上面
    CustomerType_Count
}

[Serializable]
public class CustomerData : CharacterData
{
    [SerializeField]
    private CustomerType m_Type;
    [SerializeField]
    private List<Recipe> m_PreferRecipe = new List<Recipe>();
    [SerializeField]
    private float m_RestaurantRate;

    //顾客类型
    public CustomerType Type { get => m_Type; set => m_Type = value; }
    //偏爱的菜
    public List<Recipe> PreferRecipe { get => m_PreferRecipe; set => m_PreferRecipe = value; }
    public float RestaurantRate { get => m_RestaurantRate; set => m_RestaurantRate = value; }

    public static CustomerData GenerateRandomData()
    {
        UnityEngine.Random.Range(0, 5);
        return new CustomerData();
    }
}

//对于Customer类，初步想法是先定义特殊的Customer，然后在初次运行时生成Customer表，储存在本地
//在之后的游戏过程中都是读取该表来生成Customer
public class Customer : MonoBehaviour
{
    public CustomerData Data = new CustomerData();

	void Start()
	{
	}
}
