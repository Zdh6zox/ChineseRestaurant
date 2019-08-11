using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

//GameManager需要在每个Scene里面有一个，加载Scene的时候初始化
public class GameManager : MonoBehaviour
{
    private CustomerManager m_CustomerManager;
    private RecipeManager m_RecipeManager;
    public GameDifficultySettings DifficultySettings;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        //m_CustomerManager.SpawnCustomer();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Initialize()
    {
        DifficultySettings = GameDifficultySettings.GetPresetDifficultySettings(GameDifficultySettings.GameDifficultyType.GameDifficulty_Easy);
        if (m_CustomerManager == null)
        {
            m_CustomerManager = new CustomerManager();
            m_CustomerManager.Initialize(this);

            m_RecipeManager = new RecipeManager();
            m_RecipeManager.Initialize(this);
        }
    }

    public RecipeManager GetRecipeManager()
    {
        return m_RecipeManager;
    }

    public CustomerManager GetCustomerManager()
    {
        return m_CustomerManager;
    }
}
