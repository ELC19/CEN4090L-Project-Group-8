//Same as before, tag the antagonist. Also, no sure what the player is
//tagged, just rename if needed. Gracias. Also need to attach to danger UI (i think)
using UnityEngine;
using UnityEngine.UI;

public class AntagonistAI : MonoBehavior
{
    public float moveSpeed = 0.150f; //? Not sure, grabbed camera move speed for reference
    public float slowed = 0.100f;
    public float slowDur = 1.5f;

    private bool isFlashed = false;
    public Image dangersign;
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").transform;
    }

    private void Update()
    {
        if (isFlashed)
        {
            slow();
            adjustDangerUI();
        } else if (player != null)
        {
            MoveTowardsPlayer();
            adjustDangerUI();
        }
    }
    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
    public void slow()
    {
        if(!isFlashed)
        {
            movementSpeed = slowed;
            isFlashed = true;

            StartCoroutine(restoreSpeed());
        }
    }
    IEnumerator restoreSpeed()
    {
        yield return new WaitForSeconds(slowDur);
        
        moveSpeed = 0.150f;
        isFlashed = false;
    }
    private void adjustDangerUI()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        float opacity = Mathf.Clamp01(1 - (distance / 10)); //might need to play w/ this
        dangerSign.color = new Color(1f, 1f, 1f, opacity); 
    }
}