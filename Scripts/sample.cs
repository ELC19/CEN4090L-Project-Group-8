/* Code by Logan Nyquist */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//put in OGController
void Interact()
{
    var facingDirection = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
    var interactPosition transform.position + facingDirection;

    var collider = Physics2D.OverlapCircle(interactPosition, 0.2f, interactableLayer);
    if (collider != null)
    {
        collider.GetComponent<Interactable>()?.Interact();
    }
}


public interface Interactable : MonoBehaviour
{
    void Start()
    {
        //before first frame
    }

    void Update()
    {
        //once per frame
    }

}

public class NPCController : MonoBehaviour, Interactable
{
    public void Interact()
    {
        StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));
    }
}

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

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox
    [SerializeField] Text dialogueText;
    [SerializeField] int lettersPerSecond;
    public event Action OnShowDialogue;
    public event Action OnHideDialogue;
    public static DialogueManager Instance {get; private set;}
    
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

        foreach(var letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f/lettersPerSecond);
        }
        isTyping = false;
    }

    private void Awake()
    {
        Instance = this;
    }

    Dialogue dialogue;
    int currentLine = 0;
    bool isTyping;
    private void HandleUpdate()
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
            }
        }
    }
}

[System.Serializable]
public class Dialogue
{
    [SerializeField] List<string> lines;

    public List<string> Lines
    {
        get {return lines;}
    }
}