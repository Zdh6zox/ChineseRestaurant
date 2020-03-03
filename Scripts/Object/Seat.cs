using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    [System.Serializable]
    public enum SeatType
    {
        SeatType_Normal,
        SeatType_Long
    }

    public SeatType _SeatType;
    public float _SeatSpace = 1.0f;
    public Customer _TakenCustomer;
    public Table _ParentTable;
    public bool _IsTaken = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterToTable(Table parentTable)
    {

    }

    public void UnregisterFromTable()
    {

    }

    public void AssignCustomer(Customer customer)
    {
        _IsTaken = true;
        _TakenCustomer = customer;
    }

    public void OnCustomerLeave()
    {
        _IsTaken = false;
        _TakenCustomer = null;
    }
}
