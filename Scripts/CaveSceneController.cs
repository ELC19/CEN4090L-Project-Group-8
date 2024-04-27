using System.Collections.Generic;
using UnityEngine;

public class CaveSceneController : MonoBehaviour
{
    [SerializeField] Dialogue monsterDialogue;

    void Start()
    {

        monsterDialogue.endsGame = true;  

        StartCoroutine(DialogueManager.Instance.ShowDialogue(monsterDialogue));
    }
}
