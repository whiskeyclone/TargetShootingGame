using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Wipe : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Transform wipeImageTrans;

    List<string> failMessages = new List<string>();
    List<string> winMessages = new List<string>();

    Canvas canvas;
    Vector2 startPos;
    Vector2 endPos;
    float speed = 25f;
    string wipeSound;
    float wipeSoundFadeTime = 1f;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        InitializeMessages();

        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main; // Set canvas camera
        
        GetPositions();
        wipeImageTrans.position = startPos;

        // Set text and wipe sound
        if (WinChecker.instance.GetLevelWon() == true)
        {
            text.text = GetWinMessage();
            text.color = Color.green;
            wipeSound = "Cheer";
        }
        else
        {
            text.text = GetFailMessage();
            text.color = Color.red;
            wipeSound = "Boo";
        }

        AudioController.instance.FadeInSound(wipeSound, wipeSoundFadeTime);
        StartCoroutine(Move());
    }

    void InitializeMessages()
    {
        failMessages.Add("lmao");
        failMessages.Add("cringe");
        failMessages.Add("you suck lol");
        failMessages.Add("malding");
        failMessages.Add("cope");
        failMessages.Add("just do something else bro");
        failMessages.Add("you missed");
        failMessages.Add("do you even have your eyes open?");
        failMessages.Add("it's not too late to give up");

        winMessages.Add("nice shot");
        winMessages.Add("good job");
        winMessages.Add("impressive!");
        winMessages.Add("very good");
        winMessages.Add("nice");
        winMessages.Add("keep it up!");
        winMessages.Add("what a gamer!");
    }

    string GetFailMessage()
    {
        int randIndex = Random.Range(0, failMessages.Count);
        return (failMessages[randIndex]);
    }

    string GetWinMessage()
    {
        int randIndex = Random.Range(0, winMessages.Count);
        return (winMessages[randIndex]);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        canvas.worldCamera = Camera.main; // Set canvas camera
    }

    // Get start and end positions
    void GetPositions()
    {
        float cameraWidth = MainCamera.instance.GetCameraBounds().size.x;

        startPos = new Vector2(cameraWidth, 0);
        endPos = new Vector2(-cameraWidth, 0);
    }

    // Move wipe image to center of screen, change/restart scene, then move wipe image to end position
    IEnumerator Move()
    {
        // Move to center of screen
        while (wipeImageTrans.position.x > 0f)
        {
            wipeImageTrans.position = Vector2.MoveTowards(wipeImageTrans.position, endPos, speed * Time.deltaTime);
            yield return null;
        }

        // Move to scene
        if (WinChecker.instance.GetLevelWon() == true)
        {
            SceneController.instance.GoToNextScene();
        }
        else
        {
            SceneController.instance.RestartScene();
        }

        // Move to end position
        while (wipeImageTrans.position.x > endPos.x)
        {
            wipeImageTrans.position = Vector2.MoveTowards(wipeImageTrans.position, endPos, speed * Time.deltaTime);
            yield return null;
        }

        AudioController.instance.FadeOutSound(wipeSound, wipeSoundFadeTime);       
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
