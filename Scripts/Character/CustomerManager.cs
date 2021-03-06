﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerSpawnStrategy
{
    Random
}


public class CustomerManager
{
    private GameObject m_CustomerTemplate;
    private GameManager m_GameManager;
    private List<CustomerData> m_CustomerDataList = new List<CustomerData>();
    private List<CustomerData> m_SpecialCustomerDataList = new List<CustomerData>();
    private List<GameObject> m_SpawnedCustomerCache = new List<GameObject>(); //缓存生成的顾客，用于查询

    private List<CustomerTypeInfo> m_GeneratedCustomerType;

    internal void Initialize(GameManager gameManager)
    {
        m_GameManager = gameManager;
        m_CustomerTemplate = SaveLoadManager.GetInstance().LoadCustomerTemplate();
        if (m_CustomerTemplate == null)
        {
            throw new System.Exception("Cannot load customer template,wrong path?");
        }
        string customerDataListJson = SaveLoadManager.GetInstance().LoadCustomerDataJson();
        if (customerDataListJson == "")
        {
            Debug.Log("No CustomerData Loaded, create new one");
            CreateCustomerTable();
        }
        else
        {
            m_CustomerDataList = SerializeTools.ListFromJson<CustomerData>(customerDataListJson);
        }

        //Load special customer list
        string specialCustomerDataListJson = SaveLoadManager.GetInstance().LoadSpecialCustomerDataJson();
        if (specialCustomerDataListJson != "")
        {
            List<CustomerData> specialCustomerData = SerializeTools.ListFromJson<CustomerData>(specialCustomerDataListJson);
            m_SpecialCustomerDataList.AddRange(specialCustomerData);
        }
    }

    private Customer SpawnCustomer(CustomerData spawningCustomerData, SpawnPosition seletcedSpawner)
    {
        if (seletcedSpawner == null)
            throw new System.Exception("null spawner");

        GameObject customerGO = MonoBehaviour.Instantiate(m_CustomerTemplate);
        customerGO.transform.position = seletcedSpawner.SpawnPos;
        customerGO.transform.rotation = seletcedSpawner.SpawnRot;
        Customer customerProperty = customerGO.GetComponent<Customer>();
        customerProperty.Data = spawningCustomerData;
        customerProperty.InitializeCustomer();
        m_SpawnedCustomerCache.Add(customerGO);
        return customerProperty;
    }

    private void SpawnCustomer(CustomerData spawningCustomerData, Vector3 pos, Quaternion rot)
    {
        GameObject customerGO = MonoBehaviour.Instantiate(m_CustomerTemplate);
        customerGO.transform.position = pos;
        customerGO.transform.rotation = rot;
        Customer customerProperty = customerGO.GetComponent<Customer>();
        customerProperty.Data = spawningCustomerData;
        customerProperty.InitializeCustomer();
        m_SpawnedCustomerCache.Add(customerGO);
    }

    public void SpawnRandomCustomer(SpawnPosition seletcedSpawner)
    {
        if (seletcedSpawner == null)
            return;

        CustomerData customerData = GetNextSpawningCustomer(CustomerSpawnStrategy.Random);
        Vector3 pos;
        Quaternion rot;
        seletcedSpawner.GetSpawnPosAndRot(out pos, out rot);
        SpawnCustomer(customerData, pos, rot);        
    }

    private CustomerData GetNextSpawningCustomer(CustomerSpawnStrategy strategy)
    {
        switch (strategy)
        {
            case CustomerSpawnStrategy.Random:
                int index = 0;
                int curIter = 0;
                while (true)
                {
                    curIter++;
                    if (curIter >= m_CustomerDataList.Count)
                        throw new System.Exception("Cannot spawn next customer");
                    index = Random.Range(0, m_CustomerDataList.Count);
                    CustomerData customerData = m_CustomerDataList[index];
                    if (m_SpawnedCustomerCache.Exists(x => x.GetComponent<Customer>().Data.CharacterName == customerData.CharacterName) == false)
                    {
                        return customerData;
                    }
                }
            default:
                return new CustomerData();
        }
    }

    public void SpawnCustomerGroup(CustomerSpawnStrategy strategy, int groupNum, SpawnPosition selectedSpawner)
    {
        if (selectedSpawner == null)
            return;

        CustomerData leaderData = GetNextSpawningCustomer(strategy);
        Customer leader = SpawnCustomer(leaderData, selectedSpawner);
        leader.SetAsLeader();
        Blackboard leader_bb = leader.GetComponent<Blackboard>();
        CustomerGroup group = CustomerGroup.CreateGroup(leader);
        List<Customer> members = new List<Customer>();
        //We can get positive relations from relaters, for now just get from closest relations 
        for(int i = 0;i<leaderData.PositiveRelations.Count;++i)
        {
            if (i >= groupNum - 1)
                break;

            //TODO add strategy here to choose relations
            Relationship relationShip = leaderData.PositiveRelations[i];
            CustomerData related = LookupCustomer(relationShip.WithWho);
            Customer member = SpawnCustomer(related, selectedSpawner);

            members.Add(member);

            Blackboard member_bb = member.GetComponent<Blackboard>();
            if(member_bb)
            {
                member_bb.AddOrModifyBBValue<CustomerGroup>("Group", group);
            }
        }

        group.ExpandGroup(members);

        //add group into all members' blackboard
        if(leader_bb)
        {
            leader_bb.AddOrModifyBBValue<CustomerGroup>("Group", group);
        }
    }

    public void OnCustomerUnSpawned(Customer customer)
    {
        m_SpawnedCustomerCache.Remove(customer.gameObject);
    }

    public void UnspawnAllExistingCustomer()
    {

    }

    public void CreateCustomerTable()
    {
        //第一次进入游戏时调用此函数
        Debug.Log("Creating Customer Data Table");
        m_GeneratedCustomerType = m_GameManager.DifficultySettings.NumberOfType;
        if (m_GeneratedCustomerType.Count == 0)
            throw new System.Exception("Need setup GameDifficultySettings right");

        int customerNumberNeeded = 0;
        foreach (CustomerTypeInfo info in m_GeneratedCustomerType)
        {
            customerNumberNeeded += info.Number;
        }

        for (int i = 0; i < customerNumberNeeded; ++i)
        {
            CustomerData newCustomerData = GenerateCustomerData();
            m_CustomerDataList.Add(newCustomerData);
        }

        //生成初始关系图，暂定每人的关系数量为5.
        for (int i = 0; i < m_CustomerDataList.Count; ++i)
        {
            CustomerData customerData = m_CustomerDataList[i];
            if (customerData.Relations.Count >= 5)
                continue;
            Debug.Log(string.Format("Creating RelationShip Chart for {0}", customerData.CharacterName));
            List<int> usedIndex = new List<int>();
            usedIndex.Add(i);
            //正向关系
            int positiveRelationNum = Random.Range(0, 6);
            for (int iP = 0; iP < positiveRelationNum; iP++)
            {
                while (true)
                {
                    int ranIndex = Random.Range(0, m_CustomerDataList.Count);
                    if (usedIndex.Exists(x => x == ranIndex) == false)
                    {
                        CustomerData otherCustomer = m_CustomerDataList[ranIndex];
                        if (otherCustomer.Relations.Count >= 5)
                        {
                            usedIndex.Add(ranIndex);
                            continue;
                        }

                        int positiveRelationScore = Random.Range(1, 9); //初始情况下，正向关系最大值为8                        
                        Relationship relation = new Relationship(otherCustomer.CharacterName, positiveRelationScore);
                        customerData.Relations.Add(relation);
                        customerData.PositiveRelations.Add(relation);
                        Relationship revRelation = new Relationship(customerData.CharacterName, positiveRelationScore);
                        otherCustomer.Relations.Add(revRelation);
                        otherCustomer.PositiveRelations.Add(revRelation);
                        usedIndex.Add(ranIndex);
                        break;
                    }
                }
            }

            //负向关系
            int negativeRelationNum = Random.Range(0, 6 - positiveRelationNum);
            for (int iN = 0; iN < negativeRelationNum; iN++)
            {
                while (true)
                {
                    int ranIndex = Random.Range(0, m_CustomerDataList.Count);
                    if (usedIndex.Exists(x => x == ranIndex) == false)
                    {
                        CustomerData otherCustomer = m_CustomerDataList[ranIndex];
                        if (otherCustomer.Relations.Count >= 5)
                        {
                            usedIndex.Add(ranIndex);
                            continue;
                        }

                        int negativeRelationScore = Random.Range(-8, 0); //初始情况下，负向关系最小值为-8                        
                        Relationship relation = new Relationship(otherCustomer.CharacterName, negativeRelationScore);
                        customerData.Relations.Add(relation);
                        customerData.NegativeRelations.Add(relation);
                        Relationship revRelation = new Relationship(customerData.CharacterName, negativeRelationScore);
                        otherCustomer.Relations.Add(revRelation);
                        otherCustomer.NegativeRelations.Add(relation);
                        usedIndex.Add(ranIndex);
                        break;
                    }
                }
            }
        }


        //将顾客表保存到本地
        SaveLoadManager.GetInstance().SaveCustomerDataJson(m_CustomerDataList);
    }

    public void CollectCustomizedCustomer()
    {
        //用于制作自定义顾客
    }

    private CustomerData GenerateCustomerData()
    {
        CustomerData newCustomer = new CustomerData();
        //生成顾客种类与初始携带金钱，应该与选定的游戏难度相关
        foreach (CustomerTypeInfo info in m_GeneratedCustomerType)
        {
            if (info.Number > 0)
            {
                newCustomer.Type = info.CustomerType;
                newCustomer.Money = Random.Range(info.MinMoney, info.MaxMoney + 1);
                info.Number--;
                break;
            }
        }

        //确认性别
        GenderType gender = (GenderType)Random.Range(1, 3);
        newCustomer.Gender = gender;

        //生成名字
        string name = NameGenerator.GetInstance().GenerateName(gender);
        Debug.Log(string.Format("Generated Name : {0}", name));
        newCustomer.CharacterName = name;

        //生成喜爱的菜谱
        //TBD
        //int preferRecipeNum = Random.Range(0, 6);
        //newCustomer.PreferRecipe = m_GameManager.GetRecipeManager().GetRandomRecipes(preferRecipeNum);

        //生成角色库存
        //TBD
        return newCustomer;
    }

    public CustomerData LookupCustomer(string name)
    {
        CustomerData findingCustomer = m_CustomerDataList.Find(x => x.CharacterName == name);
        if (findingCustomer != null)
            return findingCustomer;

        findingCustomer = m_SpecialCustomerDataList.Find(x => x.CharacterName == name);
        if (findingCustomer != null)
            return findingCustomer;

        return null;
    }

    public void OnCustomerDataChanged()
    {

    }
}
