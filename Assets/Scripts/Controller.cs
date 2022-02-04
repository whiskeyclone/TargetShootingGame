using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public static Controller instance;

    [SerializeField] int ignoreCollisionLayer;
    [SerializeField] GameObject wipe;
    bool levelWon = false;

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
        if (GameObject.FindGameObjectsWithTag("Wipe").Length == 0) // If there are no wipes
        {
            int targetCount = GameObject.FindGameObjectsWithTag("Target").Length;
            int bulletCount = GameObject.FindGameObjectsWithTag("Bullet").Length + PlayerController.instance.GetAmmo();

            if (targetCount == 0)
            {
                GameObject wipeInst = Instantiate(wipe, transform.position, transform.rotation); // Spawn wipe
                levelWon = true;
            }
            else if (bulletCount < targetCount)
            {
                Instantiate(wipe, transform.position, transform.rotation); // Spawn wipe
            }
        }
    }

    public bool GetLevelWon()
    {
        return (levelWon);
    }
}
