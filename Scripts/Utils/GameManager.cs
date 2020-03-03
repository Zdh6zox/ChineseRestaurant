using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

//GameManager需要在每个Scene里面有一个，加载Scene的时候初始化
public class GameManager : MonoBehaviour
{
    private CustomerManager m_CustomerManager;
    private RecipeManager m_RecipeManager;
    private InnManager m_InnManager;
    private UIManager m_UIManager;
    public GameDifficultySettings DifficultySettings;

    #region RuntimeTest
    //For Runtime UI Test
    public GameObject CurrentSpawner;
    public List<GameObject> NavTargets = new List<GameObject>();
    public GameObject CurrentNavTarget;
    #endregion

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
        }

        if(m_InnManager == null)
        {
            m_InnManager = new InnManager();
            m_InnManager.Initialize(this);
        }

        if(m_UIManager == null)
        {
            m_UIManager = new UIManager();
            m_UIManager.Initialize(this);
        }

        if(m_RecipeManager == null)
        {
            m_RecipeManager = new RecipeManager();
            m_RecipeManager.Initialize(this);
        }
    }

    public CustomerManager GetCustomerManager()
    {
        return m_CustomerManager;
    }

    public UIManager GetUIManager()
    {
        return m_UIManager;
    }

    public InnManager GetInnManager()
    {
        return m_InnManager;
    }

    public RecipeManager GetRecipeManager()
    {
        return m_RecipeManager;
    }

    #region RuntimeTest 
    //For Test API
    public void SpawnCustomer()
    {
        if (CurrentSpawner != null)
            m_CustomerManager.SpawnCustomerGroup(CustomerSpawnStrategy.Random, 3, CurrentSpawner.GetComponent<SpawnPosition>());
    }

    public void SpawnCustomerGroup()
    {
        //m_CustomerManager.SpawnCustomerGroup();
    }

    public void UnspawnCustomer()
    {
        
    }

    public void NavigateToTarget()
    {
        if (CurrentNavTarget == null)
            return;

        List<GameObject> customerGOs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Customer"));
        foreach(GameObject go in customerGOs)
        {
            Customer customer = go.GetComponent<Customer>();
            if(customer)
            {
                customer.MoveToLocation(CurrentNavTarget.transform.position);
            }
        }
    }

    public void OnNavigationTargetChange(int index)
    {
        CurrentNavTarget = NavTargets[index];
    }

    public void ShowDebug(bool isShow)
    {

    }
    #endregion
}
