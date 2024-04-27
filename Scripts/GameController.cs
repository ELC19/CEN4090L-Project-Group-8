/* Code by Logan Nyquist And Trent D. Guillen*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//freeroam: walking around
//dialogue: talking to npc
//danger: the forgotten one found you!
public enum GameState { FreeRoam, Dialogue, GameOver }

public class GameController : MonoBehaviour
{
    [SerializeField] OGController ogController;
    [SerializeField] GameObject jumpScareImage; 
    [SerializeField] float jumpScareDuration = 3f;
    GameState gameState;

    private void Start()
    {
        DialogueManager.Instance.OnShowDialogue += () =>
        { gameState = GameState.Dialogue; };
        DialogueManager.Instance.OnHideDialogue += () =>
        {
            if (gameState == GameState.Dialogue)
                gameState = GameState.FreeRoam;
        };

    }

    private void Update()
    {
        if (gameState == GameState.FreeRoam)
        {
            ogController.HandleUpdate();
            CheckInventoryCompletion();
        }

        else if (gameState == GameState.Dialogue)
            DialogueManager.Instance.HandleUpdate();

        else if (gameState == GameState.GameOver)
        {
            //AudioManager.Instance.PlayScareSound();
        }
    }

    private void CheckInventoryCompletion()
    {
        if (InventoryManager.Instance.CheckCompleteInventory())
        {
            StartCoroutine(EndGameSequence());
        }
    }

    private IEnumerator EndGameSequence()
    {
        gameState = GameState.GameOver;
        jumpScareImage.SetActive(true);
        yield return new WaitForSeconds(jumpScareDuration);
        EndGame();
    }

    private void EndGame()
    {
        jumpScareImage.SetActive(false);
        Debug.Log("Game Over");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
