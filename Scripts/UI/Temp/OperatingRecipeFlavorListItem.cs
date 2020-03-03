using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperatingRecipeFlavorListItem : MonoBehaviour
{
    private Text _amountText;
    private Text _nameText;
    private string _nameStr;
    private int _curAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Name")
            {
                _nameText = child.gameObject.GetComponent<Text>();
            }
            else if (child.name == "AmountText")
            {
                _amountText = child.gameObject.GetComponent<Text>();
            }
        }

        _nameText.text = _nameStr;
        _amountText.text = _curAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetName(string name)
    {
        _nameStr = name;
    }

    public string GetName()
    {
        return _nameStr;
    }

    public void SetCurAmount(int amount)
    {
        _curAmount = amount;
    }

    public int GetCurAmount()
    {
        return _curAmount;
    }

    public void Increase()
    {
        _curAmount++;
        if(_curAmount >= 10)
        {
            _curAmount = 10;
        }

        _amountText.text = _curAmount.ToString();
    }

    public void Decrease()
    {
        _curAmount--;
        if (_curAmount <= 0)
        {
            _curAmount = 0;
            transform.parent = null;
            Destroy(this);
        }
        else
        {
            _amountText.text = _curAmount.ToString();
        }
    }
}
