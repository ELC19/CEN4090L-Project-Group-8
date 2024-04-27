using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform spawnPoint;

    void Start()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("Number of players found: " + allPlayers.Length);
        foreach (var eachPlayer in allPlayers)
        {
            Debug.Log("Found player at position: " + eachPlayer.transform.position);
        }

        if (spawnPoint != null)
        {
            Debug.Log("Spawn point position: " + spawnPoint.position);
        }
        else
        {
            Debug.Log("Spawn point not assigned.");
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            player.transform.position = spawnPoint.position;
            Debug.Log("Player spawned at: " + spawnPoint.position);
        }
        else
        {
            Debug.Log("Player object not found. Is it tagged 'Player'?");
        }
    }


}
