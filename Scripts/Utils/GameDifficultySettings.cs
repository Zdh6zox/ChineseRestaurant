using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//每种顾客的数量以及携带金钱上下限，与游戏难度有关
[System.Serializable]
public class CustomerTypeInfo
{
    [SerializeField]
    public CustomerType CustomerType;
    [SerializeField]
    public int Number;
    [SerializeField]
    public int MaxMoney;
    [SerializeField]
    public int MinMoney;

    public CustomerTypeInfo(CustomerType customerType, int number, int maxMoney, int minMoney)
    {
        CustomerType = customerType;
        Number = number;
        MaxMoney = maxMoney;
        MinMoney = minMoney;
    }
}

[System.Serializable]
public class GameDifficultySettings
{
    public enum GameDifficultyType
    {
        GameDifficulty_Easy,
        GameDifficulty_Normal,
        GameDifficulty_Hard
    }

    public GameDifficultyType Difficulty;
    [SerializeField]
    public List<CustomerTypeInfo> NumberOfType;

    private static GameDifficultySettings Easy = new GameDifficultySettings(GameDifficultyType.GameDifficulty_Easy, new List<CustomerTypeInfo> { new CustomerTypeInfo(CustomerType.CustomerType_Beggar, 10, 100, 0),
        new CustomerTypeInfo(CustomerType.CustomerType_Citizen, 50, 500, 100),
        new CustomerTypeInfo(CustomerType.CustomerType_Councillor, 10, 4000, 3000),
        new CustomerTypeInfo(CustomerType.CustomerType_Escort, 10, 2000, 500),
        new CustomerTypeInfo(CustomerType.CustomerType_Hooligan, 15, 500, 100),
        new CustomerTypeInfo(CustomerType.CustomerType_Mafia, 10, 800, 300),
        new CustomerTypeInfo(CustomerType.CustomerType_Merchant, 10, 4000, 500),
        new CustomerTypeInfo(CustomerType.CustomerType_Peasant, 50, 300, 50),
        new CustomerTypeInfo(CustomerType.CustomerType_Swordman, 30, 500, 100)});
    private static GameDifficultySettings Normal;
    private static GameDifficultySettings Hard;

    public GameDifficultySettings(GameDifficultyType difficulty, List<CustomerTypeInfo> numberOfType)
    {
        Difficulty = difficulty;
        NumberOfType = numberOfType;
    }

    public static GameDifficultySettings GetPresetDifficultySettings(GameDifficultyType type)
    {
        //现在只返回Easy
        return Easy;
    }
}
