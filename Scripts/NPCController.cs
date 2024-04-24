/* Code by Logan Nyquist */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NPCController : MonoBehaviour, Interactable
{
    public void Interact()
    {
        StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));
    }
}