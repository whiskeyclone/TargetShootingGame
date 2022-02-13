using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] GameObject menuWipe;
    public void StartGame()
    {
        if (GameObject.FindGameObjectsWithTag("Wipe").Length == 0) // If no wipes exist
        {
            // Wipe to level 1
            Instantiate(menuWipe);

            StatController.instance.StartTimer();
            AudioController.instance.PlaySound("Laser");
        }
    }
}
