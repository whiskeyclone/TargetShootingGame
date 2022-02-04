using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        Controller.instance.CheckWin();
    }
}
