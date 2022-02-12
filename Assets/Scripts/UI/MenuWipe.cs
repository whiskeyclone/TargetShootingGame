using UnityEngine;
using System.Collections;

public class MenuWipe : MonoBehaviour
{
    Vector2 startPos;
    Vector2 endPos;
    float speed = 25f;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        GetPositions();
        transform.position = startPos;

        StartCoroutine(Move());
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
        while (transform.position.x > 0f)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            yield return null;
        }

        Controller.instance.GoToNextScene();

        // Move to end position
        while (transform.position.x > endPos.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}
