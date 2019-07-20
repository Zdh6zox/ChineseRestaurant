using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GenderType : uint
{
    GenderType_Male,   //男性
    GenderType_Female  //女性
}

public class Character : MonoBehaviour
{
    [SerializeField]
    private string m_CharacterName;
    [SerializeField]
    private string m_Description;
    [SerializeField]
    private GenderType m_Gender;
    [SerializeField]
    private Inventory m_Inventroy;
    [SerializeField]
    private List<Relationship> m_Relations = new List<Relationship>(); //目前使用Name当Key,使用int来表示关系，范围为 [-10,10]

    public string CharacterName { get => m_CharacterName; set => m_CharacterName = value; }
    public string Description { get => m_Description; set => m_Description = value; }
    public GenderType Gender { get => m_Gender; set => m_Gender = value; }
    public Inventory Inventroy { get => m_Inventroy; set => m_Inventroy = value; }
    public List<Relationship> Relations { get => m_Relations; set => m_Relations = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
