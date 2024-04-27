using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OGController : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;

    private Vector2 input;

    private Animator animator;

    public LayerMask solidObjectsLayer;
    public LayerMask InteractableLayer;

    private Inventory inventory;

    void Start()
    {

        Vector2 spawnPosition;

        if (PlayerPrefs.HasKey("FixedSpawnPosX") && PlayerPrefs.HasKey("FixedSpawnPosY"))
        {
            spawnPosition = new Vector2(
                PlayerPrefs.GetFloat("FixedSpawnPosX"),
                PlayerPrefs.GetFloat("FixedSpawnPosY")
            );

            PlayerPrefs.DeleteKey("FixedSpawnPosX");
            PlayerPrefs.DeleteKey("FixedSpawnPosY");
        }
        else
        {
            spawnPosition = new Vector2(
                PlayerPrefs.GetFloat("PlayerPosX", 0), 
                PlayerPrefs.GetFloat("PlayerPosY", 0)
            );
        }

        transform.position = spawnPosition;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();

        inventory = new Inventory();

    }


    public void HandleUpdate()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //Debug.Log("This is input.x" + input.x);
            //Debug.Log("This is unput.y" + input.y);

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;


                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }

        animator.SetBool("isMoving", isMoving);
        
        if (Input.GetKeyDown(KeyCode.I))
            Interact();

        if (Input.GetKeyDown(KeyCode.P))
        {
            DialogueManager.Instance.ToggleInventoryDisplay();
        }


        if (Input.GetKeyDown(KeyCode.O))
        {
            // Check for items around the player or within reach
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1.0f);
            foreach (var hitCollider in hitColliders)
            {
                ItemWorld itemWorld = hitCollider.GetComponent<ItemWorld>();
                if (itemWorld != null)
                {
                    inventory.AddItem(itemWorld.GetItem());
                    itemWorld.DestroySelf();
                }
            }
        }
        
    }

    
    void Interact()
    {
        var facingDirection = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPosition = transform.position + facingDirection;

        Debug.DrawLine(transform.position, interactPosition, Color.red, 1f);

        var collider = Physics2D.OverlapCircle(interactPosition, 0.2f, InteractableLayer);
        if (collider != null)
        {
            Debug.Log("interaction");
            Debug.Log($"Collider found: {collider.gameObject.name}");
            var interactable = collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
            else
            {
                Debug.Log("Interactable component not found on " + collider.gameObject.name);
                // Check if there are any components of type Interactable
                var interactables = collider.GetComponents<Interactable>();
                Debug.Log("Number of Interactable components: " + interactables.Length);
            }
        }
        else
        {
            Debug.Log("No interactable collider found.");
        }
    }
    

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | InteractableLayer) != null)
        {
            return false;
        }

        return true;
    }
}

