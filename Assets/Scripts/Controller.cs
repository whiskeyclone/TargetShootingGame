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
        Debug.Log(targetsLeft);
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
