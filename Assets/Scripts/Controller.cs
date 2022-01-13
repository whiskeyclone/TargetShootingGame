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

    private void Update()
    {
        int targetsLeft = GameObject.FindGameObjectsWithTag("Target").Length;

        // Advance to next scene when no targets are left
        if (targetsLeft == 0)
        {
            // Get next scene
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) // Check if next scene is valid
            {
                // Go to next scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
