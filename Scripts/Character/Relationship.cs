using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用来储存Relationship的data
[Serializable]
public class Relationship
{
    [SerializeField]
    private string m_WithWho;
    [SerializeField]
    private int m_RelationScore; //范围为[-10, 10]

    public string WithWho { get => m_WithWho; set => m_WithWho = value; }
    public int RelationScore { get => m_RelationScore; set => m_RelationScore = value; }
}
