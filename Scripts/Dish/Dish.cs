using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//菜品类，Dish类是实际做出来的菜肴
[System.Serializable]
public class DishData
{
    public Recipe _Recipe;
}

public class Dish : MonoBehaviour
{
    public DishData _Data;
}

