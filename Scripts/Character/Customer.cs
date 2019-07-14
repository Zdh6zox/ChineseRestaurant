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


//对于Customer类，初步想法是先定义特殊的Customer，然后在初次运行时生成Customer表，储存在本地
//在之后的游戏过程中都是读取该表来生成Customer
public class Customer : Character
{
    private CustomerType m_Type;
    private List<Recipe> m_PreferRecipe;

    //顾客类型
    public CustomerType Type { get => m_Type; set => m_Type = value; }
    //偏爱的菜
    public List<Recipe> PreferRecipe { get => m_PreferRecipe; set => m_PreferRecipe = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
