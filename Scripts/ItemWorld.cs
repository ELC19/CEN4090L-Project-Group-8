using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld: MonoBehaviour {

    private ItemWorld item;

    private SpriteRenderer spriteRenderer;
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetItem(ItemWorld item) {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();

    }

    public ItemWorld GetItem(){
        return item;

    }

    public void DestroySelf(){
        DestroySelf(gameObject);
    }

}

