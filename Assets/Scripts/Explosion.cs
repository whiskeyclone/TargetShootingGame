using UnityEngine;

public class Explosion : MonoBehaviour
{
    private bool targetHit; // Whether or not the bullet that spawned this explosion hit a target

    private void OnParticleSystemStopped()
    {
        if (targetHit == true)
        {
            Controller.instance.CheckWin();
        }
        else
        {
            Controller.instance.RestartScene();
        }
    }

    public void SetTargetHit(bool x)
    {
        targetHit = x;
    }
}
