/*Necessary objects in scene:
 UI Image that covers entire screen- for flashImage
 Interactable layer over animals- enables detection & removal via Interact() (need code for transferring to journal)
Animator- self explanatory
antagonist- If we add, make sure to label so we can photo while he's on screen too.
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Numerics;
using static Item;

public class CameraBehavior : MonoBehaviour
{
    public Animator animator;
    public LayerMask InteractableLayer;
    public float flashduration = 0.5f;
    public Color flashcolor = new Color(1f, 1f, 1f, 1f);
    public GameObject antagonist;
    public Image flashImage;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    void Interact()
    {
        UnityEngine.Vector2 facingDir = new UnityEngine.Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        UnityEngine.Vector3 interactPos = transform.position + new UnityEngine.Vector3(facingDir.x, facingDir.y, 0);

        Collider2D collider = Physics2D.OverlapCircle(interactPos, 0.2f, LayerMask.GetMask("InteractableLayer"));
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
        else
        {
            Debug.Log("No interactable collider found at interact position.");
        }
    }

    IEnumerator FlashAndCollect(Collider2D animalCollider)
    {
        StartCoroutine(FlashEffect());
        yield return new WaitForSeconds(flashduration);
        AddItemToInventory(animalCollider.tag);
        Destroy(animalCollider.gameObject);
    }

    bool IsAnimalTag(string tag)
    {
        return tag == "Blackbear" || tag == "Deer" || tag == "Panther" || tag == "Bullfrog" || tag == "Otter" || tag == "Alligator" || tag == "Spoonbill" || tag == "Yellowbelly" || tag == "Watermoccasin";
    }

    bool IsAntagonistVisible()
    {
        UnityEngine.Vector3 screenPoint = Camera.main.WorldToViewportPoint(antagonist.transform.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    public void TakePhoto()
    {
        StartCoroutine(FlashEffect());
    }

    IEnumerator FlashEffect()
    {
        flashImage.color = flashcolor;
        flashImage.enabled = true;
        float fadeSpeed = 1f / flashduration;
        while (flashImage.color.a > 0)
        {
            Color newColor = flashImage.color;
            newColor.a -= fadeSpeed * Time.deltaTime;
            flashImage.color = newColor;
            yield return null;
        }
        flashImage.enabled = false;
    }

    IEnumerator WaitForEEE(string objectTag, float duration)
    {
        bool eKeyPressed = false;
        float timer = 0f;
        while (timer < duration && !eKeyPressed)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                eKeyPressed = true;
            }

            timer += Time.deltaTime;
            yield return null;
        }
        if (eKeyPressed)
        {
            AddItemToInventory(objectTag);
        }
        Destroy(GetComponent<Collider>().gameObject);

    }

    void FlashAntagonist()
    {
        antagonist.GetComponent<AntagonistAI>().Slow();
    }

    public void AddItemToInventory(string tag)
    {
        Inventory inventory = InventoryManager.Instance.inventory;
        Item.ItemType? itemType = MapTagToItemType(tag);
        if (itemType.HasValue) 
        {
            inventory.AddItem(new Item { itemType = itemType.Value, amount = 1 });
            Debug.Log("Added " + itemType.Value.ToString() + " to inventory.");
        }
        else
        {
            Debug.Log("No valid item type found for tag: " + tag);
        }
    }

    Item.ItemType? MapTagToItemType(string tag)
    {
        switch (tag)
        {
            case "Flower":
                return Item.ItemType.Flower;
            case "Key":
                return Item.ItemType.Key;
            case "Deer":
                return Item.ItemType.Deer;
            case "Panther":
                return Item.ItemType.Panther;
            case "Alligator":
                return Item.ItemType.Alligator;
            case "Spoonbill":
                return Item.ItemType.Spoonbill;
            case "Bullfrog":
                return Item.ItemType.Bullfrog;
            case "Blackbear":
                return Item.ItemType.Blackbear;
            case "Otter":
                return Item.ItemType.Otter;
            case "Yellowbelly":
                return Item.ItemType.Yellowbelly;
            case "Watermocassin":
                return Item.ItemType.Watermocassin;
            default:
                return null;
        }
    }
}
