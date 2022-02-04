using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Wipe : MonoBehaviour
{
    Vector2 startPos;
    Vector2 endPos;
    Vector2 cameraPos;
    float speed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        GetPositions();
        transform.position = startPos;
        ScaleToScreen();

        SceneManager.sceneLoaded += OnSceneLoaded;

        StartCoroutine(MoveToCenter());
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(MoveToEnd());
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

    // Move to center of screen then change/restart scene
    IEnumerator MoveToCenter()
    {
        // Move to center of screen
        while (transform.position.x > cameraPos.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            yield return null;
        }

        // Change or restart scene
        if (Controller.instance.GetLevelWon() == true)
        {
            Controller.instance.GoToNextScene();
        }
        else
        {
            Controller.instance.RestartScene();
        }
    }

    // Move to end position then destroy self
    IEnumerator MoveToEnd()
    {
        // Update positions and move to center of screen
        GetPositions();
        transform.position = cameraPos;

        // Move to end position
        while (transform.position.x > endPos.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
