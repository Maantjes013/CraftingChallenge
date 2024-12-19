using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BankUI : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private TMP_Text levelText;

    [SerializeField]
    private Transform inventoryItemsParent;

    [SerializeField]
    private int sellScore = 0;

    private List<Item> sellList = new List<Item>();

    public void FillBankUI()
    {
        sellScore = 0;
        sellList.Clear();
        scoreText.text = PlayerInventory.Instance.playerScore.ToString() + " Points";
        levelText.text = "Level " + Mathf.RoundToInt(PlayerInventory.Instance.playerScore / 50).ToString();
        foreach (Transform child in inventoryItemsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in PlayerInventory.Instance.playerItems)
        {
            Image instantiatedImage = Instantiate(itemImage, inventoryItemsParent);
            instantiatedImage.sprite = item.itemSprite;
            instantiatedImage.GetComponent<Button>().onClick.AddListener(() => OnItemClick(item, instantiatedImage));
        }
    }

    private void OnItemClick(Item clickedItem, Image instantiatedImage)
    {
        if (sellList.Any(x => x.itemID.Equals(clickedItem.itemID)))
            RemoveItemFromSellList(clickedItem, instantiatedImage);
        else
            AddItemToSellList(clickedItem, instantiatedImage);
    }

    private void AddItemToSellList(Item itemToAdd, Image instantiatedImage)
    {
        sellList.Add(itemToAdd);
        instantiatedImage.color = Color.red;
        sellScore += (int)(itemToAdd.itemTier) * 20;
        scoreText.text = $"{PlayerInventory.Instance.playerScore} + {sellScore} Points";
    }

    private void RemoveItemFromSellList(Item itemToRemove, Image instantiatedImage)
    {
        sellList.RemoveAll(x => x.itemID.Equals(itemToRemove.itemID));
        instantiatedImage.color = Color.white;
        sellScore -= (int)(itemToRemove.itemTier) * 20;

        if (sellScore == 0)
            scoreText.text = PlayerInventory.Instance.playerScore + " Points";
        else
            scoreText.text = $"{PlayerInventory.Instance.playerScore} + {sellScore} Points";
    }

    public void SellItems()
    {
        PlayerInventory.Instance.playerScore += sellScore;

        foreach (Item itemToRemove in sellList)
        {
            PlayerInventory.Instance.playerItems.Remove(itemToRemove);
        }

        FillBankUI();
    }

    private void OnDisable()
    {
        sellScore = 0;
        sellList.Clear();
    }
}
