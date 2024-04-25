using Systems.Collections;
using Systems.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehavior {

public static ItemAssets Instance { get; private set;}

private void Awake(){
    Instance = this;
}
public Sprite flowerSprite;
public Sprite keySprite;
public Sprite deerSprite;
public Sprite pantherSprite;
public Sprite alligatorSprite;
public Sprite spoonbillSprite;
public Sprite bullfrogSprite;
public Sprite blackbearSprite;
public Sprite otterSprite;
public Sprite yellowbellySprite;

}
