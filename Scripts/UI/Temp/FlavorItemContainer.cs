using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlavorItemContainer : MonoBehaviour
{
    private Button _addBtn;
    private Button _reduceBtn;
    private Text _nameText;

    private RecipeRecordManager _manager;

    private string _nameStr;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "AddButton")
            {
                _addBtn = child.gameObject.GetComponent<Button>();
                _addBtn.onClick.AddListener(this.OnAddBtnPressed);
            }
            else if (child.name == "ReduceButton")
            {
                _reduceBtn = child.gameObject.GetComponent<Button>();
                _reduceBtn.onClick.AddListener(this.OnReduceBtnPressed);
            }
            else if (child.name == "ItemName")
            {
                _nameText = child.gameObject.GetComponent<Text>();
                _nameText.text = _nameStr;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(RecipeRecordManager manager,string name)
    {
        _manager = manager;
        _nameStr = name;
    }

    public void OnAddBtnPressed()
    {
        _manager.AddFlavorOnCurRecipe(this);
    }

    public void OnReduceBtnPressed()
    {
        _manager.ReduceFlavorOnCurRecipe(this);
    }

    public string GetName()
    {
        return _nameStr;
    }
}
