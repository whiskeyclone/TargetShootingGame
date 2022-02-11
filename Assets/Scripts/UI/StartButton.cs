using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void StartGame()
    {
        MainCamera.instance.SpawnMenuWipe();
    }
}
