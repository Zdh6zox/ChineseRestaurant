using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeRecordManager : MonoBehaviour
{
    public List<Recipe> ConstructedRecipes;

    Recipe _curOperatingRecipe = new Recipe();

    //public delegate void ChangeOperatingRecipe(RecipeNameItem item);

    //public ChangeOperatingRecipe recipeChangeCallBack;
    public GameObject RecipeNameTemplate;
    public GameObject OperatingListItemTemplate;
    public GameObject ListItemContainerTemplate;

    ScrollRect _recipeNameView;
    ScrollRect _flavorNameView;
    ScrollRect _cookMethodNameView;
    ScrollRect _constructedRecipeView;

    Button _saveBtn;
    Button _exportBtn;

    ScrollRect _operatingRecipeFlavourList;
    List<OperatingRecipeFlavorListItem> _curFlavorList = new List<OperatingRecipeFlavorListItem>();
    ScrollRect _operatingRecipeCookMethodList;
    //List<OperatingRecipeListItem> _curCookMethodList = new List<OperatingRecipeListItem>();
    Text _operatingRecipeName;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            if(child.name == "RecipeNameList")
            {
                _recipeNameView = child.gameObject.GetComponent<ScrollRect>();
            }
            else if(child.name == "FlavourList")
            {
                _flavorNameView = child.gameObject.GetComponent<ScrollRect>();
            }
            else if(child.name == "CookMethodNameList")
            {
                _cookMethodNameView = child.gameObject.GetComponent<ScrollRect>();
            }
            else if(child.name == "OperatingRecipeFlavourList")
            {
                _operatingRecipeFlavourList = child.gameObject.GetComponent<ScrollRect>();
            }
            else if(child.name == "OperatingRecipeCookMethodList")
            {
                _operatingRecipeCookMethodList = child.gameObject.GetComponent<ScrollRect>();
            }
            else if(child.name == "OperatingRecipeName")
            {
                _operatingRecipeName = child.gameObject.GetComponent<Text>();
            }
            else if(child.name == "ConstructedRecipeList")
            {
                _constructedRecipeView = child.gameObject.GetComponent<ScrollRect>();
            }
            else if(child.name == "SaveBtn")
            {
                _saveBtn = child.gameObject.GetComponent<Button>();
                _saveBtn.onClick.AddListener(this.SaveCurOperatingRecipe);
            }
            else if(child.name == "ExportBtn")
            {
                _exportBtn = child.gameObject.GetComponent<Button>();
                _exportBtn.onClick.AddListener(this.ExportRecipes);
            }
        }

        ConstructRecipeNameList();
        ConstructFlavourList();
        BuildConstructedRecipeList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConstructRecipeNameList()
    {
        List<string> recipeNames = LoadRecipeNameString();
        foreach(string recipeName in recipeNames)
        {
            GameObject recipeGO = GameObject.Instantiate(RecipeNameTemplate);
            recipeGO.transform.parent = _recipeNameView.content;
            RecipeNameItem item = recipeGO.GetComponent<RecipeNameItem>();
            item.Init(this,recipeName);
        }
    }

    public void BuildConstructedRecipeList()
    {
        LoadConstructedRecipeList();
        foreach (Recipe recipe in ConstructedRecipes)
        {
            GameObject recipeGO = GameObject.Instantiate(RecipeNameTemplate);
            recipeGO.transform.parent = _constructedRecipeView.content;
            RecipeNameItem item = recipeGO.GetComponent<RecipeNameItem>();
            item.Init(this, recipe.Name);
        }
    }

    public void LoadConstructedRecipeList()
    {
        string recipeStr = SaveLoadManager.GetInstance().LoadRecipeJson();
        List<Recipe> recipes = SerializeTools.ListFromJson<Recipe>(recipeStr);
        ConstructedRecipes = recipes;
    }

    public void ConstructFlavourList()
    {
        List<string> flavorNames = LoadFlavourNameString();
        foreach(string flavorName in flavorNames)
        {
            GameObject flavorGO = GameObject.Instantiate(ListItemContainerTemplate);
            flavorGO.transform.parent = _flavorNameView.content;
            FlavorItemContainer container = flavorGO.GetComponent<FlavorItemContainer>();
            container.Init(this, flavorName);
        }
    }

    public List<string> LoadRecipeNameString()
    {
        string recipeNames = SaveLoadManager.GetInstance().LoadRecipeName();

        string[] names = recipeNames.Split(',');
        List<string> nameList = new List<string>(names);

        return nameList;
    }

    public List<string> LoadFlavourNameString()
    {
        string flavorNames = SaveLoadManager.GetInstance().LoadFlavourName();

        string[] names = flavorNames.Split(',');
        List<string> nameList = new List<string>(names);

        return nameList;
    }

    public List<string> LoadCookMethodString()
    {
        string cookNames = SaveLoadManager.GetInstance().LoadCookMethodName();

        string[] names = cookNames.Split(',');
        List<string> nameList = new List<string>(names);

        return nameList;
    }

    public void ChangeOperatingNewRecipe(RecipeNameItem item)
    {
        string recipeName = item.GetRecipeName();

        //RecipeManager recipeManager = Utils.GetGameManager().GetRecipeManager();
        //RecipeQueryRequest quest = new RecipeQueryRequest();
        //quest._amountType = RecipeQueryRequest.AmountType.Single;
        //quest._queryType = RecipeQueryRequest.QueryType.Name;
        //quest._nameToFound = recipeName;
        //RecipeQueryResult result = recipeManager.LookUpRecipeInLibrary(in quest);
        //if(result._type == RecipeQueryResult.Type.Exist)
        //{
        //    _curOperatingRecipe = result.GetBestRecipe();
        //}
        //else if(result._type == RecipeQueryResult.Type.Fail)
        //{
        //    _curOperatingRecipe = new Recipe();
        //    _curOperatingRecipe.Name = recipeName;
        //}

        if (ConstructedRecipes.Exists(x => x.Name == recipeName))
        {
            _curOperatingRecipe = ConstructedRecipes.Find(x => x.Name == recipeName);
        }
        else
        {
            _curOperatingRecipe = new Recipe();
            _curOperatingRecipe.Name = recipeName;
        }

        List<FlavourFactor> flavors = _curOperatingRecipe.Flavours;
        foreach(OperatingRecipeFlavorListItem flavorItem in _curFlavorList)
        {
            Destroy(flavorItem.gameObject);
        }
        _curFlavorList.Clear();
        foreach(FlavourFactor flavour in flavors)
        {
            AddNewFlavourItem(flavour.Name, flavour.FactorDegree);
        }
        _operatingRecipeName.text = _curOperatingRecipe.Name;
    }

    private void AddNewFlavourItem(string factorName,int amount)
    {
        GameObject newListItem = GameObject.Instantiate(OperatingListItemTemplate);
        OperatingRecipeFlavorListItem listItem = newListItem.GetComponent<OperatingRecipeFlavorListItem>();
        listItem.SetName(factorName);
        listItem.SetCurAmount(amount);
        _curFlavorList.Add(listItem);

        newListItem.transform.parent = _operatingRecipeFlavourList.content;
    }

    private void ModifyExistingFlavourItem(string factorName,bool isIncrease)
    {
        if(isIncrease)
        {
            foreach (OperatingRecipeFlavorListItem item in _curFlavorList)
            {
                if (item.GetName() == factorName)
                {
                    item.Increase();
                    break;
                }
            }
        }
        else
        {
            foreach (OperatingRecipeFlavorListItem item in _curFlavorList)
            {
                if (item.GetName() == factorName)
                {
                    item.Decrease();
                    if(item.GetCurAmount() <=0)
                    {
                        _curFlavorList.Remove(item);
                    }
                    break;
                }
            }
        }
    }

    public void AddFlavorOnCurRecipe(FlavorItemContainer listItem)
    {
        bool isFound = false;
        foreach (OperatingRecipeFlavorListItem item in _curFlavorList)
        {
            if (item.GetName() == listItem.GetName())
            {
                isFound = true;
                break;
            }
        }

        if(isFound)
        {
            ModifyExistingFlavourItem(listItem.GetName(), true);
        }
        else
        {
            AddNewFlavourItem(listItem.GetName(), 1);
        }
    }

    public void ReduceFlavorOnCurRecipe(FlavorItemContainer listItem)
    {
        bool isFound = false;
        foreach (OperatingRecipeFlavorListItem item in _curFlavorList)
        {
            if (item.GetName() == listItem.GetName())
            {
                isFound = true;
                break;
            }
        }

        if (isFound)
        {
            ModifyExistingFlavourItem(listItem.GetName(), false);
        }
    }

    public void AddCookMethodOnCurRecipe(CookMethodItemContainer item)
    {

    }

    public void ReduceCookMethodOnCurRecipe(CookMethodItemContainer item)
    {

    }

    public void SaveCurOperatingRecipe()
    {
        Recipe newRecipe;
        bool isAdd = false;
        if (ConstructedRecipes.Exists(x => x.Name == _operatingRecipeName.text))
        {
            newRecipe = ConstructedRecipes.Find(x => x.Name == _operatingRecipeName.text);
            isAdd = false;
        }
        else
        {
            newRecipe = new Recipe();
            newRecipe.Name = _operatingRecipeName.text;
            isAdd = true;
        }

        newRecipe.Flavours.Clear();
        //export flavor
        foreach(OperatingRecipeFlavorListItem item in _curFlavorList)
        {
            FlavourFactor flavor = new FlavourFactor();
            flavor.Name = item.GetName();
            flavor.FactorDegree = item.GetCurAmount();
            newRecipe.Flavours.Add(flavor);
        }

        //TODO:
        //export cook method

        if(isAdd)
        {
            ConstructedRecipes.Add(newRecipe);
            ModifyConstructedRecipeList(newRecipe, isAdd);
        }
    }

    public void ModifyConstructedRecipeList(Recipe recipe, bool isAdd)
    {
        if(isAdd)
        {
            GameObject recipeGO = GameObject.Instantiate(RecipeNameTemplate);
            recipeGO.transform.parent = _constructedRecipeView.content;
            RecipeNameItem item = recipeGO.GetComponent<RecipeNameItem>();
            item.Init(this, recipe.Name);
        }
    }


    public void ExportRecipes()
    {
        SaveLoadManager.GetInstance().SaveRecipeJson(ConstructedRecipes);
    }
}
