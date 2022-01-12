using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public static Controller instance;

    [SerializeField] int ignoreCollisionLayer;
    int targetsLeft;

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

        // Get number of targets in scene
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        targetsLeft = targets.Length;
    }

    public void DecrementTargetsLeft()
    {
        if (targetsLeft > 0)
        {
            targetsLeft--;
        }
        else
        {
            Debug.LogError("Cannot decrement targetsLeft because targetsLeft is zero!");
        }
    }

    private void Update()
    {
        // Advance to next scene when no targets are left
        if (targetsLeft == 0)
        {
            // Get next scene
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) // Check if there is a next scene
            {
                // Go to next scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
