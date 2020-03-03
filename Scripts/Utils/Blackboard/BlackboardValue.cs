using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum BlackboardValueType
{
    ValueType_SignedInt,
    ValueType_UnsignedInt,
    ValueType_Bool,
    ValueType_Float,
    ValueType_Vector3,
    ValueType_Quternion,
    ValueType_Table,
    ValueType_Customer,
    ValueType_CustomerList,
    ValueType_CustomerGroup,
    ValueType_GameObject
}


[System.Serializable]
public class BlackboardValue
{
    public BlackboardValueType _Type;

    private int _SignedIntValue;
    private uint _UnsignedIntValue;
    private bool _BoolValue;
    private float _FloatValue;
    private Vector3 _Vector3Value;
    private Quaternion _QuternionValue;
    private GameObject _GameObjectValue;
    private Customer _CustomerValue;
    private Table _TableValue;
    private CustomerGroup _CustomerGroupValue;
    private List<Customer> _CustomerListValue;

    public T GetValue<T>()
    {
        if(typeof(T).Equals(typeof(int)))
        {
            return (T)(object)_SignedIntValue;
        }
        else if(typeof(T).Equals(typeof(uint)))
        {
            return (T)(object)_UnsignedIntValue;
        }
        else if (typeof(T).Equals(typeof(bool)))
        {
            return (T)(object)_BoolValue;
        }
        else if (typeof(T).Equals(typeof(float)))
        {
            return (T)(object)_FloatValue;
        }
        else if (typeof(T).Equals(typeof(Vector3)))
        {
            return (T)(object)_Vector3Value;
        }
        else if (typeof(T).Equals(typeof(Quaternion)))
        {
            return (T)(object)_QuternionValue;
        }
        else if (typeof(T).Equals(typeof(GameObject)))
        {
            return (T)(object)_GameObjectValue;
        }
        else if (typeof(T).Equals(typeof(Customer)))
        {
            return (T)(object)_CustomerValue;
        }
        else if (typeof(T).Equals(typeof(Table)))
        {
            return (T)(object)_TableValue;
        }
        else if (typeof(T).Equals(typeof(CustomerGroup)))
        {
            return (T)(object)_CustomerGroupValue;
        }
        else if (typeof(T).Equals(typeof(List<Customer>)))
        {
            return (T)(object)_CustomerListValue;
        }
        else
        {
            throw new System.Exception("Try to get not implemented BlackValue type");
        }
    }

    public void SetValue<T>(T value)
    {
        if (value is int)
        {
            _Type = BlackboardValueType.ValueType_SignedInt;
            _SignedIntValue = (int)(object)value;
        }
        else if (value is uint)
        {
            _Type = BlackboardValueType.ValueType_UnsignedInt;
            _UnsignedIntValue = (uint)(object)value;
        }
        else if (value is bool)
        {
            _Type = BlackboardValueType.ValueType_Bool;
            _BoolValue = (bool)(object)value;
        }
        else if (value is float)
        {
            _Type = BlackboardValueType.ValueType_Float;
            _FloatValue = (float)(object)value;
        }
        else if (value is Vector3)
        {
            _Type = BlackboardValueType.ValueType_Vector3;
            _Vector3Value = (Vector3)(object)value;
        }
        else if (value is Quaternion)
        {
            _Type = BlackboardValueType.ValueType_Quternion;
            _QuternionValue = (Quaternion)(object)value;
        }
        else if (value is GameObject)
        {
            _Type = BlackboardValueType.ValueType_GameObject;
            _GameObjectValue = (GameObject)(object)value;
        }
        else if (value is Table)
        {
            _Type = BlackboardValueType.ValueType_Table;
            _TableValue = (Table)(object)value;
        }
        else if (value is Customer)
        {
            _Type = BlackboardValueType.ValueType_Customer;
            _CustomerValue = (Customer)(object)value;
        }
        else if (value is List<Customer>)
        {
            _Type = BlackboardValueType.ValueType_CustomerList;
            _CustomerListValue = (List<Customer>)(object)value;
        }
        else if (value is CustomerGroup)
        {
            _Type = BlackboardValueType.ValueType_CustomerGroup;
            _CustomerGroupValue = (CustomerGroup)(object)value;
        }
        else
        {
            throw new System.Exception("Try to set not implemented BlackValue type");
        }
    }
}
