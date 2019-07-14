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
    private string m_Name;
    private string m_Description;
    private GenderType m_Gender;

    public string Name { get => m_Name; set => m_Name = value; }
    public string Description { get => m_Description; set => m_Description = value; }
    public GenderType Gender { get => m_Gender; set => m_Gender = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
