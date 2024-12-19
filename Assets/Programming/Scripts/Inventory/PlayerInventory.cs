using System;
using System.Collections.Generic;

public class PlayerInventory : MonoSingleton<PlayerInventory>
{
    public List<Item> playerItems;
    public int playerScore;
    private int maxInventoryItems = 10;

    public bool TryAddItemToInventory(Item pickedUpItem)
    {
        if (playerItems.Count >= maxInventoryItems)
            return false;

        playerItems.Add(pickedUpItem);
        return true;
    }
}
