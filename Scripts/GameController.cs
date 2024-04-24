/* Code by Logan Nyquist */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//freeroam: walking around
//dialogue: talking to npc
//danger: the forgotten one found you!
public enum GameState {FreeRoam, Dialogue, Danger}

public class GameController : MonoBehaviour 
{
    [SerializeField] OGController ogController;
    GameState gameState;
    private void Start()
    {
        DialogueManager.Instance.OnShowDialogue += () =>
        {gameState = GameState.Dialogue;};
        DialogueManager.Instance.OnHideDialogue += () =>
        {
            if (gameState == GameState.Dialogue)
                gameState = GameState.FreeRoam;
        };

    }
    private void Update()
    {
        if (gameState = GameState.FreeRoam)
            ogController.HandleUpdate();

        else if (gameState = GameState.Dialogue)
            DialogueManager.Instance.HandleUpdate();
            
        else if (gameState = GameState.Danger)
    }
}