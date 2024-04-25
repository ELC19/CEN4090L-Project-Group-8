using Systems.Collections;
using Systems.Collections.Generic;
using UnityEngine;

public class Inventory {

    public event EventHandler OnItemListChanged;

    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = itemList.ItemType.Flower, amount = 1});
        AddItem(new Item { itemType = itemList.ItemType.Key, amount = 1});
        AddItem(new Item { itemType = itemList.ItemType.Deer, amount = 1});
        AddItem(new Item { itemType = itemList.ItemType.Panther, amount = 1});
        AddItem(new Item { itemType = itemList.ItemType.Alligator, amount = 1});
        AddItem(new Item { itemType = itemList.ItemType.Spoonbill, amount = 1});
        AddItem(new Item { itemType = itemList.ItemType.Bullfrog, amount = 1});
        AddItem(new Item { itemType = itemList.ItemType.Blackbear, amount = 1});
        AddItem(new Item { itemType = itemList.ItemType.Otter, amount = 1});
        AddItem(new Item { itemType = itemList.ItemType.Yellowbelly, amount = 1});
    
    }

    public void AddItem(Item item) {
        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        
    }

    public List<Item> GetItemList() {
        return itemList;
    }
}