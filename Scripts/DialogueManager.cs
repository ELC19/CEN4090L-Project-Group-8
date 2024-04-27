/* Code by Logan Nyquist */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] Text dialogueText;
    [SerializeField] int lettersPerSecond;
    public event Action OnShowDialogue;
    public event Action OnHideDialogue;
    private bool isInventoryShown = false;
    public static DialogueManager Instance { get; private set; }
    Dialogue dialogue;
    int currentLine = 0;
    bool isTyping;

    public IEnumerator ShowDialogue(Dialogue dialogue)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialogue?.Invoke();
        this.dialogue = dialogue;
        dialogueBox.SetActive(true);
        StartCoroutine(TypeDialogue(dialogue.Lines[0]));
    }

    public IEnumerator TypeDialogue(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (var letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {

            if (++currentLine < dialogue.Lines.Count)
            {
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));
            }
            else
            {
                dialogueBox.SetActive(false);
                currentLine = 0;
                OnHideDialogue?.Invoke();

                if (dialogue.endsGame)  
                {
                    EndGame(); 
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
            HideInventory();
    }

    public void ShowInventory()
    {
        Debug.Log("Showing Inventory");
        if (InventoryManager.Instance != null && InventoryManager.Instance.inventory != null)
        {
            var items = InventoryManager.Instance.inventory.GetItemList(); 
            string inventoryText = "Inventory: ";
            foreach (var item in items)
            {
                inventoryText += item.itemType.ToString() + " x" + item.amount + ", ";
            }

            if (items.Count > 0)
                inventoryText = inventoryText.TrimEnd(',', ' ');
            else
                inventoryText += "empty";

            DisplayInventoryText(inventoryText);
            isInventoryShown = true;
        }
        else
        {
            Debug.LogError("No InventoryManager found or no inventory available.");
        }
        Debug.Log("Inventory Shown");

        
    }

    public void ToggleInventoryDisplay()
    {
        if (isInventoryShown)
        {
            HideInventory();
        }
        else
        {
            ShowInventory();
        }
    }

    private void DisplayInventoryText(string text)
    {
        OnShowDialogue?.Invoke();
        dialogueBox.SetActive(true);
        dialogueText.text = text; 
    }

    private void HideInventory()
    {
        Debug.Log("Hiding Inventory");
        dialogueBox.SetActive(false);
        isInventoryShown = false;
        OnHideDialogue?.Invoke();
        Debug.Log("Inventory Hidden");
    }

    private void EndGame()
    {
        Debug.Log("Game Over - The monster has eaten you!");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
