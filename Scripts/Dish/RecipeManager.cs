using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager
{
    private List<Recipe> m_Recipes;
    private GameManager m_GameManager;

    public void Initialize(GameManager gameManager)
    {
        m_GameManager = gameManager;
    }

    public Recipe LookUpRecipe(string name)
    {
        foreach(Recipe recipe in m_Recipes)
        {
            if(recipe.Name == name)
            {
                return recipe;
            }
        }

        throw new System.Exception(string.Format("Cannot find Recipe:{0}", name));
    }

    public Recipe GetRandomRecipe()
    {
        int ranIndex = Random.Range(0, m_Recipes.Count);
        return m_Recipes[ranIndex];
    }

    public List<Recipe> GetRandomRecipes(int Num)
    {
        List<Recipe> recipes = new List<Recipe>();
        return recipes;
    }

    private void GenerateRecipe()
    {

    }
}
