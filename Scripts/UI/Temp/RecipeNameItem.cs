using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeNameItem : MonoBehaviour
{
    Text _name;
    Button _btn;
    string _nameStr;
    RecipeRecordManager _manager;
    // Start is called before the first frame update
    void Start()
    {
        _name = GetComponentInChildren<Text>();
        _btn = GetComponent<Button>();
        _name.text = _nameStr;
        _btn.onClick.AddListener(OnBtnPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(RecipeRecordManager manager, string name)
    {
        _manager = manager;
        _nameStr = name;
    }

    public void OnBtnPressed()
    {
        _manager.ChangeOperatingNewRecipe(this);
    }

    public string GetRecipeName()
    {
        return _nameStr;
    }
}
