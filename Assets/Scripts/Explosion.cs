using UnityEngine;

public class Explosion : MonoBehaviour
{
    private bool targetHit; // Whether or not the bullet that spawned this explosion hit a target

    private void OnParticleSystemStopped()
    {
        Controller.instance.CheckWin();
    }
}
