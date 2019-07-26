using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject CustomerTemplate;
    public Transform TestTrans;
    private List<CustomerData> m_CustomerDataList;
    private List<GameObject> m_SpawnedCustomerCache = new List<GameObject>(); //缓存生成的顾客，用于查询

    // Start is called before the first frame update
    void Start()
    {
		//TestSpawn();
		TestSaveFirstNameTable();
		TestLoadFirstNameTable();
    }

    // Update is called once per frame
    void Update()
    {
        
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


    #region TestSpawn
    void TestSpawn()
    {
        CustomerData cum1 = new CustomerData();
        cum1.CharacterName = "张三";
        cum1.Gender = GenderType.GenderType_Male;
        CustomerData cum2 = new CustomerData();
        cum2.CharacterName = "李四";
        cum2.Relations.Add(new Relationship("张三", 10));
        cum2.Description = "不是张三";
        cum2.Gender = GenderType.GenderType_Male;
        CustomerData cum3 = new CustomerData();
        cum3.CharacterName = "王二姐";
        cum3.Gender = GenderType.GenderType_Female;

        List<CustomerData> cumList = new List<CustomerData>();
        cumList.Add(cum1);
        cumList.Add(cum2);
        cumList.Add(cum3);
        m_CustomerDataList = cumList;
        StartCoroutine(SpawnCustomers(cumList, 1));
    }
	#endregion

	#region TestFirstNameTable
	private void TestSaveFirstNameTable()
	{
		List<FirstName> testList = new List<FirstName>();
		FirstName fn = new FirstName("马冬梅",0.8f);
		testList.Add(fn);
		SaveLoadManager.GetInstance().SaveFirstNameJson(testList);
	}

	private void TestLoadFirstNameTable()
	{
		string firstNameTable = SaveLoadManager.GetInstance().LoadFirstNameJson();
		List<FirstName> testList = SerializeTools.ListFromJson<FirstName>(firstNameTable);
		Debug.Log(testList[0].Weight);
	}
	#endregion
}
