using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public static Controller instance;

    [SerializeField] int ignoreCollisionLayer;

    // Start is called before the first frame update
    void Start()
    {
        // Set instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this); // Destroy instance if another instance exists
            return;
        }

        // Ignore collisions between objects on ignoreCollisionLayer
        Physics2D.IgnoreLayerCollision(ignoreCollisionLayer, ignoreCollisionLayer);
    }

    public void GoToNextScene()
    {
        // Get next scene
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) // Check if next scene is valid
        {
            // Go to next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // If there are no targets left, move to next scene. Otherwise, if it is impossible for the player to win, restart scene
    public void CheckWin()
    {
        int targetCount = GameObject.FindGameObjectsWithTag("Target").Length;
        int bulletCount = GameObject.FindGameObjectsWithTag("Bullet").Length + PlayerController.instance.GetAmmo();

        if (targetCount == 0)
        {
            GoToNextScene();
        }
        else if (bulletCount < targetCount)
        {
            RestartScene();
        }
    }
}
