using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard : MonoBehaviour
{
    [SerializeField]
    public Dictionary<string, BlackboardValue> _Dic = new Dictionary<string, BlackboardValue>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddOrModifyBBValue<T>(string key, T value)
    {
        if (_Dic.ContainsKey(key))
        {
            BlackboardValue existingValue;
            _Dic.TryGetValue(key, out existingValue);
            existingValue.SetValue<T>(value);
        }
        else
        {
            BlackboardValue newValue = new BlackboardValue();
            newValue.SetValue<T>(value);
            _Dic.Add(key, newValue);
        }
    }

    public bool GetBBValue<T>(string key, out T value)
    {
        if (_Dic.ContainsKey(key) == false)
        {
            value = default(T);
            return false;
        }

        BlackboardValue existingValue;
        _Dic.TryGetValue(key, out existingValue);
        value = existingValue.GetValue<T>();
        return true;
    }
}

