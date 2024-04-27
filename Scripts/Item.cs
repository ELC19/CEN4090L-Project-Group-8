//Emily Cleveland
//attach script to flower and rock/key
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{

    public enum ItemType
    {
        Flower,
        Key,
        Deer,
        Panther,
        Alligator,
        Spoonbill,
        Bullfrog,
        Blackbear,
        Otter,
        Yellowbelly,
        Watermocassin,
    }

    public ItemType itemType;
    public int amount;

    
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Flower: return ItemAssets.Instance.flowerSprite;
            case ItemType.Key: return ItemAssets.Instance.keySprite;
            case ItemType.Deer: return ItemAssets.Instance.deerSprite;
            case ItemType.Panther: return ItemAssets.Instance.pantherSprite;
            case ItemType.Alligator: return ItemAssets.Instance.alligatorSprite;
            case ItemType.Spoonbill: return ItemAssets.Instance.spoonbillSprite;
            case ItemType.Bullfrog: return ItemAssets.Instance.bullfrogSprite;
            case ItemType.Blackbear: return ItemAssets.Instance.blackbearSprite;
            case ItemType.Otter: return ItemAssets.Instance.otterSprite;
            case ItemType.Yellowbelly: return ItemAssets.Instance.yellowbellySprite;
            case ItemType.Watermocassin: return ItemAssets.Instance.watermocassin;


        }

    }
}
