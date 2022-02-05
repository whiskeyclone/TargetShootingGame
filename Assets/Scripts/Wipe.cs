using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wipe : MonoBehaviour
{
    Vector2 startPos;
    Vector2 endPos;
    float speed = 40f;
    List<string> failMessages = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        InitializeFailMessages();

        GetPositions();
        transform.position = startPos;

        StartCoroutine(Move());
    }

    void InitializeFailMessages()
    {
        failMessages.Add("lmao");
        failMessages.Add("cringe");
        failMessages.Add("you suck lol");
        failMessages.Add("malding");
        failMessages.Add("just do something else bro");
        failMessages.Add("you missed");
        failMessages.Add("do you even have your eyes open?");
    }

    // Get start and end positions
    void GetPositions()
    {
        float cameraWidth = MainCamera.instance.GetCameraBounds().size.x;

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

        // Change or restart scene
        if (Controller.instance.GetLevelWon() == true)
        {
            Controller.instance.GoToNextScene();
        }
        else
        {
            Controller.instance.RestartScene();
        }

        // Move to end position
        while (transform.position.x > endPos.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}
