using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad; // Имя сцены для загрузки

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger zone.");
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.Log("Another object entered the trigger zone: " + other.name);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is in the trigger zone.");
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.Log("Another object is in the trigger zone: " + other.name);
        }
    }
}
