﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//味道因子类，菜肴的味道由味道因子决定。
//调味料、原材料都会有味道因子
[Serializable]
public class FlavourFactor
{
    [SerializeField]
    private string m_Name;
    [SerializeField]
    private int m_factorDegree;

    public string Name { get => m_Name; set => m_Name = value; }
    public int FactorDegree { get => m_factorDegree; set => m_factorDegree = value; }
}
