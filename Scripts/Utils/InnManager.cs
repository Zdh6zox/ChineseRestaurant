using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnManager
{
    public List<Table> _AvaliableTables = new List<Table>();

    private RecipeManager _recipeManager;
    private GameManager _gameManager;

    public void Initialize(GameManager gameManager)
    {
        _gameManager = gameManager;
        List<GameObject> existingTables = new List<GameObject>(GameObject.FindGameObjectsWithTag("Table"));

        _recipeManager = new RecipeManager();
        _recipeManager.Initialize(gameManager);

        foreach(GameObject existingTableGO in existingTables)
        {
            Table table = existingTableGO.GetComponent<Table>();
            if(table != null)
            {
                _AvaliableTables.Add(table);
            }
        }
    }

    public void RegisterTable(Table newTable)
    {
        if(_AvaliableTables.Contains(newTable) == false)
        {
            _AvaliableTables.Add(newTable);
        }
    }

    public bool QueryTable(Customer leader,out Table foundTable)
    {
        if (_AvaliableTables.Count == 0)
            throw new System.Exception("No Table in this inn");

        //TMP, return the table has enough seats
        CustomerGroup group = CustomerGroup.GetGroupViaLeader(leader);

        int groupSize = group.GetGroupSize();

        foreach(Table table in _AvaliableTables)
        {
            if(table.GetAvaliableSeatsCount() >= groupSize)
            {
                foundTable = table;
                return true;
            }
        }

        foundTable = null;
        return false;
    }

    //Recipe Menu related
    public RecipeQueryResult QueryMenu(Customer requester)
    {
        //TODO:
        //用requester的data来创建QueryRequest
        RecipeQueryRequest request = new RecipeQueryRequest();
        return _recipeManager.QueryMenu(in request);
    }

    //Maybe we can have several exit depends on the situation ?
    public Vector3 GetExitLocation()
    {
        //Temp, Use Spawner in GameManager.
        //TODO: need store spawn location in InnManager
        return Utils.GetGameManager().CurrentSpawner.transform.position;
    }
}
