using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Recipe", menuName = "CraftingChallenge/Recipe")]
public class Recipe : ScriptableObject
{
    public List<Item> recipeItems;

    public Item createdItem;
}
