using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Controller : MonoBehaviour
{
    public static Controller instance;

    [SerializeField] int ignoreCollisionLayer;
    bool levelWon = false;
    float time = 0f;
    int resetCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        // Set instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy instance if another instance exists
            return;
        }

        Physics2D.IgnoreLayerCollision(ignoreCollisionLayer, ignoreCollisionLayer); // Ignore collisions between objects on ignoreCollisionLayer
        AudioController.instance.PlaySound("Main Song");
    }

    public void GoToNextScene()
    {
        // Get next scene
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) // Check if next scene is valid
        {
            // Go to next scene
            levelWon = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);          
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        levelWon = false;
        resetCount++;
    }

    public int GetCurrentSceneIndex()
    {
        return (SceneManager.GetActiveScene().buildIndex);
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
                MainCamera.instance.SpawnWipe();
                levelWon = true;
            }
            else if (bulletCount < targetCount)
            {
                MainCamera.instance.SpawnWipe();
            }
        }
    }

    public bool GetLevelWon()
    {
        return (levelWon);
    }

    public void StartTimer()
    {
        time = 0f;
        StartCoroutine(Timer());
    }

    public void StopTimer()
    {
        StopCoroutine(Timer());
    }

    public float GetTime()
    {
        return (time);
    }

    IEnumerator Timer()
    {
        while (true)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }

    public int GetResetCount()
    {
        return (resetCount);
    }

    public void ResetResetCount()
    {
        resetCount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GoToNextScene();
        }
    }
}
