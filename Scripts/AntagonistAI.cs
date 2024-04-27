
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AntagonistAI : MonoBehaviour
{
    public float moveSpeed = 0.150f;
    public float slowed = 0.100f;
    public float slowDur = 1.5f;

    private bool isFlashed = false;
    public Image dangerSign;  

    private Transform player; 

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    private void Update()
    {
        if (isFlashed)
        {
            Slow();
            AdjustDangerUI();
        }
        else if (player != null) 
        {
            MoveTowardsPlayer();
            AdjustDangerUI();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    public void Slow()
    {
        if (!isFlashed)
        {
            moveSpeed = slowed;
            isFlashed = true;
            StartCoroutine(RestoreSpeed());
        }
    }

    IEnumerator RestoreSpeed()
    {
        yield return new WaitForSeconds(slowDur);
        moveSpeed = 0.150f;
        isFlashed = false;
    }

    private void AdjustDangerUI()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        float opacity = Mathf.Clamp01(1 - (distance / 10));
        dangerSign.color = new Color(1f, 1f, 1f, opacity);
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(TriggerJumpscare());
        }
    }

    IEnumerator TriggerJumpscare()
    {
        // Activate jumpscare image or animation
        jumpscareImage.SetActive(true);

        // Wait for a few seconds
        yield return new WaitForSeconds(2);

        // Deactivate the image or end the animation
        jumpscareImage.SetActive(false);

        // Optionally, reset the game or change the game state
    }
    */
}
