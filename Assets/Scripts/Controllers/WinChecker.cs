using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WinChecker : MonoBehaviour
{
    public static WinChecker instance;
    [SerializeField] GameObject wipe;

    bool levelWon = false;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

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
            Destroy(gameObject); // Destroy instance if another instance exists
            return;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        levelWon = false;
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
                // Player won level
                levelWon = true;
                Instantiate(wipe);
            }
            else if (bulletCount < targetCount)
            {
                // Player lost level
                Instantiate(wipe);
                StatController.instance.SetResetCount(StatController.instance.GetResetCount() + 1); // Increase reset count
            }
        }
    }

    public bool GetLevelWon()
    {
        return (levelWon);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
