using UnityEngine;
using TMPro;

public class EndMenu : MonoBehaviour
{
    [SerializeField] GameObject menuWipe;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI resetText;

    // Start is called before the first frame update
    void Start()
    {
        StatController.instance.StopTimer();
        DisplayStats();
        StatController.instance.SetResetCount(0);
    }

    void DisplayStats()
    {
        int resetCount = StatController.instance.GetResetCount();

        // Calculate hours, minutes, and seconds
        float time = Mathf.Round(StatController.instance.GetTime());
        float hours = 0f;
        float minutes = 0f;
        float seconds = 0f;

        if (time < 60)
        {
            seconds = time;
        }
        else if (time < 3600)
        {
            minutes = Mathf.Floor(time / 60);
            seconds = time - (minutes * 60);
        }
        else
        {
            hours = Mathf.Floor(time / 3600);
            minutes = Mathf.Floor((time - (hours * 3600)) / 60);
            seconds = time - (hours * 3600) - (minutes * 60);
        }

        // Display text
        timeText.text += hours + " h / " + minutes + " m / " + seconds + " s";
        resetText.text += resetCount;
    }

    public void RestartGame()
    {
        Instantiate(menuWipe);
        AudioController.instance.PlaySound("Laser");
    }
}
