using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory 
{

    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Flower, amount = 0 });
        AddItem(new Item { itemType = Item.ItemType.Key, amount = 0 });
        AddItem(new Item { itemType = Item.ItemType.Deer, amount = 0 });
        AddItem(new Item { itemType = Item.ItemType.Panther, amount = 0 });
        AddItem(new Item { itemType = Item.ItemType.Alligator, amount = 0 });
        AddItem(new Item { itemType = Item.ItemType.Spoonbill, amount = 0 });
        AddItem(new Item { itemType = Item.ItemType.Bullfrog, amount = 0 });
        AddItem(new Item { itemType = Item.ItemType.Blackbear, amount = 0 });
        AddItem(new Item { itemType = Item.ItemType.Otter, amount = 0 });
        AddItem(new Item { itemType = Item.ItemType.Yellowbelly, amount = 0 });
        AddItem(new Item { itemType = Item.ItemType.Watermocassin, amount = 0 });

        Debug.Log(itemList.Count);

    }

    public void AddItem(Item item)
    {
        var existingItem = itemList.Find(x => x.itemType == item.itemType);
        if (existingItem != null)
        {
            existingItem.amount += item.amount;
        }
        else
        {
            itemList.Add(item);
        }
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
