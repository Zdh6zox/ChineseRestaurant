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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region TestCode
    void TestToJson()
    {
        CharacterName = "张三";
        Description = "张三就是张三";
        Gender = GenderType.GenderType_Male;
        Relationship tmpRelationship = new Relationship();
        tmpRelationship.WithWho = "李四";
        tmpRelationship.RelationScore = 10;
        Relations.Add(tmpRelationship);
        m_Type = CustomerType.CustomerType_Beggar;
        FlavourFactor factor1 = new FlavourFactor();
        factor1.Name = "甜";
        FlavourFactor factor2 = new FlavourFactor();
        factor2.Name = "酸";
        FlavourFactor factor3 = new FlavourFactor();
        factor3.Name = "辣";
        FlavourFactor factor4 = new FlavourFactor();
        factor4.Name = "咸";
        Recipe testRecipe1 = new Recipe();
        testRecipe1.Type = RecipeType.RecipeType_Chuan;
        testRecipe1.Name = "麻婆豆腐";
        List<FlavourFactor> list1 = new List<FlavourFactor>();
        list1.Add(factor1);
        list1.Add(factor3);
        List<FlavourFactor> list2 = new List<FlavourFactor>();
        list2.Add(factor1);
        list2.Add(factor4);
        testRecipe1.Flavours = list1;
        testRecipe1.FlavourMods = list2;
        Recipe testRecipe2 = new Recipe();
        testRecipe2.Type = RecipeType.RecipeType_Chuan;
        testRecipe2.Name = "开水白菜";
        testRecipe2.Flavours = list2;

        m_PreferRecipe.Add(testRecipe1);
        m_PreferRecipe.Add(testRecipe2);
        string testStr = JsonUtility.ToJson(this);
        Debug.Log(testStr);
    }
    #endregion
}
