using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//烹调方式类。
//做饭方式结果会影响最终味道
[Serializable]
public class CookMethod
{
    [SerializeField]
    private string m_Name;
    [SerializeField]
    private int m_Duration;

    public string Name { get => m_Name; set => m_Name = value; }
    public int Duration { get => m_Duration; set => m_Duration = value; }
}


//TODO:
//完成 CookMethodResult类。
//类中心思想为 将原料或者CookMethodResult 通过CookMethod生成中间产物Result
//最后的菜肴通过Result的组合完成
public class CookMethodResult
{

}

