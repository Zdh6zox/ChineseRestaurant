using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    private static CustomerManager m_Instance;

    public GameObject CustomerTemplate;
    public Transform TestTrans;
    private List<CustomerData> m_CustomerDataList;
    private List<GameObject> m_SpawnedCustomerCache = new List<GameObject>(); //缓存生成的顾客，用于查询


    public CustomerManager()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static CustomerManager GetInstance()
    {
        if(m_Instance == null)
        {
            m_Instance = new CustomerManager();
        }

        return m_Instance;
    }

    public void SpawnCustomer()
    {

    }

    public void LoadCustomerTable()
    {

    }

    public void CreateCustomerTable()
    {
        //第一次进入游戏时调用此函数
    }

    public void CollectCustomizedCustomer()
    {
        //用于制作自定义顾客
    }

    public bool SpawnCustomerGroup()
    {
        //外部调用spawn接口
        return false;
    }

    private IEnumerator SpawnCustomers(List<CustomerData> spawningCustomers,int spawnTime) 
    {
        for(int i =0;i< spawningCustomers.Count; i++)
        {
            GameObject customerGO = Instantiate(CustomerTemplate, TestTrans.position,TestTrans.rotation);
            Customer customerProperty = customerGO.GetComponent<Customer>();
            customerProperty.Data = spawningCustomers[i];
            m_SpawnedCustomerCache.Add(customerGO);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
