using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] GameObject menuWipe;
    public void StartGame()
    {
        if (GameObject.FindGameObjectsWithTag("Wipe").Length == 0) // If no wipes exist
        {
            Instantiate(menuWipe);

            Controller.instance.StartTimer();
            Controller.instance.ResetResetCount();

            AudioController.instance.PlaySound("Laser");
        }
    }
}
