using System.Collections;
using UnityEngine;

public class AnimalController : MonoBehaviour, Interactable
{
    public Item.ItemType itemType;  

    public void Interact()
    {
        Debug.Log("Animal interacted: " + gameObject.name);
        TakePhotoAndCollect();
    }

    void TakePhotoAndCollect()
    {
        CameraBehavior camera = FindObjectOfType<CameraBehavior>();
        if (camera != null)
        {
            camera.TakePhoto();
            camera.AddItemToInventory(itemType.ToString());
            StartCoroutine(Disappear());
        }
        else
        {
            Debug.LogError("No CameraBehavior found in the scene!");
        }
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(0.5f); 
        Destroy(gameObject);
    }
}
