using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        WinChecker.instance.CheckWin();
        Destroy(gameObject);
    }
}
