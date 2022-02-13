using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuWipe : MonoBehaviour
{
    [SerializeField] Transform wipeImageTrans;
    Canvas canvas;
    Vector2 startPos;
    Vector2 endPos;
    float speed = 25f;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main; // Set canvas camera

        GetPositions();
        wipeImageTrans.position = startPos;

        StartCoroutine(Move());
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        canvas.worldCamera = Camera.main; // Set canvas camera
    }

    // Get start and end positions
    void GetPositions()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = screenAspect * cameraHeight;

        startPos = new Vector2(cameraWidth, 0);
        endPos = new Vector2(-cameraWidth, 0);
    }

    // Move to center of screen, change/restart scene, then move to end position
    IEnumerator Move()
    {
        // Move to center of screen
        while (wipeImageTrans.position.x > 0f)
        {
            wipeImageTrans.position = Vector2.MoveTowards(wipeImageTrans.position, endPos, speed * Time.deltaTime);
            yield return null;
        }

        SceneController.instance.GoToScene(1); // Go to first level

        // Move to end position
        while (wipeImageTrans.position.x > endPos.x)
        {
            wipeImageTrans.position = Vector2.MoveTowards(wipeImageTrans.position, endPos, speed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
