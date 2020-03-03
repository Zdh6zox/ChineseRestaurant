using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeQueryRequest
{
    public enum QueryType
    {
        Name,
        RecipeType,
        Flavour
    }

    public enum AmountType
    {
        Single,
        List
    }

    public QueryType _queryType;
    public AmountType _amountType;
    public string _nameToFound;
    public RecipeType _recipeTypeToFound;
    public List<FlavourFactor> _factorsToFound;

    public float _scoreTolerance;
}


public class RecipeQueryResultItem
{
    public float _score;
    public Recipe _recipe;

    public static RecipeQueryResultItem CreateResultItem(in RecipeQueryRequest request,in Recipe queryingRecipe)
    {
        //TODO:
        //Implement query algorithm
        RecipeQueryResultItem item = new RecipeQueryResultItem();
        bool findList = request._amountType == RecipeQueryRequest.AmountType.List;
        switch (request._queryType)
        {
            case RecipeQueryRequest.QueryType.Name:
                if(findList)
                {

                }
                else
                {
                    if (queryingRecipe.Name == request._nameToFound)
                    {
                        item._recipe = queryingRecipe;
                        item._score = 100;
                    }
                }
                break;
            case RecipeQueryRequest.QueryType.RecipeType:
                break;
            case RecipeQueryRequest.QueryType.Flavour:
                break;
        }

        if(item._score >= request._scoreTolerance)
        {
            return item;
        }

        return null;
    }
}


public class RecipeQueryResult
{
    public enum Type
    {
        Exist,
        HasSimilar,
        Fail
    }

    public List<RecipeQueryResultItem> _foundItems = new List<RecipeQueryResultItem>();
    public Type _type;


    public static RecipeQueryResult CreateRecipeQueryResult(in List<RecipeQueryResultItem> itemList)
    {
        RecipeQueryResult result = new RecipeQueryResult();
        if(itemList.Count == 0)
        {
            result._type = Type.Fail;
        }
        else if(itemList.Count == 1)
        {
            result._type = Type.Exist;
            result._foundItems = itemList;
        }
        else
        {
            result._type = Type.HasSimilar;
            result._foundItems = itemList;
        }

        return result;
    }

    public Recipe GetBestRecipe()
    {
        float maxScore = 0;
        Recipe foundRecipe = Recipe.GetInvalid();
        foreach(RecipeQueryResultItem item in _foundItems)
        {
            if(item._score > maxScore)
            {
                foundRecipe = item._recipe;
                maxScore = item._score;
            }
        }

        return foundRecipe;
    }

}


public class RecipeManager
{
    private List<Recipe> m_RecipeLibrary = new List<Recipe>();

    private List<Recipe> m_InnRecipeMenu = new List<Recipe>();
    private GameManager m_GameManager;

    public void Initialize(GameManager gameManager)
    {
        m_GameManager = gameManager;


        //string recipeJson = SaveLoadManager.GetInstance().LoadRecipeJson();
        //m_BasicRecipes = SerializeTools.ListFromJson<Recipe>(recipeJson);
    }

    public RecipeQueryResult LookUpRecipeInLibrary(in RecipeQueryRequest request)
    {
        List<RecipeQueryResultItem> resultItemList = new List<RecipeQueryResultItem>();
        foreach (Recipe recipe in m_RecipeLibrary)
        {
            RecipeQueryResultItem item = RecipeQueryResultItem.CreateResultItem(in request, in recipe);
            if(item != null)
            {
                resultItemList.Add(item);
            }
        }

        return RecipeQueryResult.CreateRecipeQueryResult(in resultItemList);
    }

    public RecipeQueryResult QueryMenu(in RecipeQueryRequest request)
    {

        RecipeQueryResult result = new RecipeQueryResult();
        result._type = RecipeQueryResult.Type.Fail;
        return result;
    }

    public Recipe GetRandomRecipe()
    {
        int ranIndex = Random.Range(0, m_RecipeLibrary.Count);
        return m_RecipeLibrary[ranIndex];
    }

    public List<Recipe> GetRandomRecipes(int Num)
    {
        List<Recipe> recipes = new List<Recipe>();
        return recipes;
    }
}
