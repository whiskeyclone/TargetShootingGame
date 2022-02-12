using UnityEngine;
using TMPro;

public class EndMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI resetText;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(MainCamera.instance.gameObject); // Destroy main camera
        Controller.instance.StopTimer();
        DisplayStats();
    }

    void DisplayStats()
    {
        float time = Controller.instance.GetTime();
        int resetCount = Controller.instance.GetResetCount();

        timeText.text += time;
        resetText.text += resetCount;
    }
}
