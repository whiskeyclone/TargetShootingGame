using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Wipe : MonoBehaviour
{
    Vector2 startPos;
    Vector2 endPos;
    Vector2 cameraPos;
    float speed = 30f;
    int nextSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        GetPositions();
        transform.position = startPos;
        ScaleToScreen();

        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Check that nextSceneIndex is valid
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("There is no next scene!");
        }

        StartCoroutine(Move());
    }

    // Scale object so it covers the entire screen
    void ScaleToScreen()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * screenAspect;

        transform.localScale = new Vector2(cameraWidth, cameraHeight);
    }

    // Get start and end positions
    void GetPositions()
    {
        cameraPos = Camera.main.transform.position;

        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * screenAspect;

        startPos = cameraPos + new Vector2(cameraWidth, 0);
        endPos = cameraPos + new Vector2(-cameraWidth, 0);
    }

    IEnumerator Move()
    {
        // Move to center of screen
        while (transform.position.x > cameraPos.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            yield return null;
        }

        SceneManager.LoadScene(nextSceneIndex); // Move to next scene

        // Wait for scene to load
        while (SceneManager.GetActiveScene().buildIndex != nextSceneIndex)
        {
            yield return null;
        }

        // Update positions and move to center of screen
        GetPositions();
        transform.position = Camera.main.transform.position;

        // Move to end position
        while (transform.position.x > endPos.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}
