using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CraftUI : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private Image equalsSignImagePrefab;

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private TMP_Text levelText;

    [SerializeField]
    private Transform inventoryItemsParent;

    [SerializeField]
    private Transform craftsParent;

    [SerializeField]
    private List<Recipe> recipes;

    [SerializeField]
    private Transform selectedRecipeParent;

    [SerializeField]
    private Button craftButton;

    private Recipe selectedRecipe;

    private void Awake()
    {
        FillRecipes();
    }

    public void FillInventoryItems()
    {
        scoreText.text = PlayerInventory.Instance.playerScore + " Points";
        levelText.text = "Level " + Mathf.RoundToInt(PlayerInventory.Instance.playerScore / 50).ToString();
        inventoryItemsParent.DestroyChildren();

        foreach (Item item in PlayerInventory.Instance.playerItems)
        {
            Image instantiatedItem = Instantiate(itemImage, inventoryItemsParent);
            instantiatedItem.sprite = item.itemSprite;
        }
    }

    private void FillRecipes()
    {
        craftsParent.DestroyChildren();

        foreach (Recipe recipe in recipes)
        {
            Image instantiatedItem = Instantiate(itemImage, craftsParent);
            instantiatedItem.sprite = recipe.createdItem.itemSprite;
            instantiatedItem.GetComponent<Button>().onClick.AddListener(() => ShowRecipe(recipe));
        }
    }

    private void ShowRecipe(Recipe recipe)
    {
        selectedRecipe = recipe;
        selectedRecipeParent.DestroyChildren();

        Image itemToCraftImage = Instantiate(itemImage, selectedRecipeParent);
        itemToCraftImage.sprite = recipe.createdItem.itemSprite;

        Instantiate(equalsSignImagePrefab, selectedRecipeParent);

        foreach (Item item in recipe.recipeItems)
        {
            Image instantiatedImage = Instantiate(itemImage, selectedRecipeParent);
            instantiatedImage.sprite = item.itemSprite;
            instantiatedImage.color = PlayerInventory.Instance.playerItems.Contains(item) ? Color.green : Color.red;
        }

        craftButton.interactable = recipe.recipeItems.All(item => PlayerInventory.Instance.playerItems.Contains(item));
    }

    public void CraftItem()
    {
        Item itemClone = (Item)selectedRecipe.createdItem.Clone();
        itemClone.itemID = System.Guid.NewGuid().ToString();

        PlayerInventory.Instance.playerItems.Add(itemClone);
        foreach (Item itemToRemove in selectedRecipe.recipeItems)
        {
            PlayerInventory.Instance.playerItems.Remove(itemToRemove);
        }

        FillInventoryItems();
        ShowRecipe(selectedRecipe);
    }

    private void OnDisable()
    {
        selectedRecipeParent.DestroyChildren();
    }
}
