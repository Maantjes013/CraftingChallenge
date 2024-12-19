using System;
using System.Linq;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CraftingChallenge/Item")]
public class Item : ScriptableObject, ICloneable
{
    [HideInInspector]
    public string itemID;

    public ItemType itemType;
    public ItemTier itemTier;
    public Mesh itemMesh;
    public Material itemMaterial;
    public Sprite itemSprite;

    public void FillItemPrefab(Item itemToCopy)
    {
        itemID = itemToCopy.itemID;
        itemType = itemToCopy.itemType;
        itemTier = itemToCopy.itemTier;
        itemSprite = itemToCopy.itemSprite;
    }

    public object Clone()
    {
        return this.MemberwiseClone() as Item;
    }
}
