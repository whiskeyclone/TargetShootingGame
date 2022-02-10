using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Wipe : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    Vector2 startPos;
    Vector2 endPos;
    float speed = 25f;
    List<string> failMessages = new List<string>();
    string wipeSound;

    // Start is called before the first frame update
    void Start()
    {
        InitializeFailMessages();

        if (Controller.instance.GetLevelWon() == false)
        {
            SelectRandomFailMessage();
        }
        
        GetPositions();
        transform.position = startPos;
        
        // Get wipe sound
        if (Controller.instance.GetLevelWon() == true)
        {
            wipeSound = "Cheer";
        }
        else
        {
            wipeSound = "Boo";
        }

        AudioController.instance.FadeInSound(wipeSound);

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

    void SelectRandomFailMessage()
    {
        int randIndex = Random.Range(0, failMessages.Count);
        text.text = failMessages[randIndex];
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

        AudioController.instance.FadeOutSound(wipeSound);
        Destroy(gameObject);
    }
}
