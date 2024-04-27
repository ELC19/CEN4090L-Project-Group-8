using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableCaveEntrance : MonoBehaviour, Interactable
{
    public string sceneToLoad = "SampleScene"; 

    public void Interact()
    {
        Debug.Log("Interacting with cave entrance, loading scene: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
