using UnityEngine;
using System.Collections;

public class StatController : MonoBehaviour
{
    public static StatController instance;

    private void Start()
    {
        // Set instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy instance if another instance exists
            return;
        }
    }

    float time = 0f;
    int resetCount = 0;

    public void StartTimer()
    {
        time = 0f;
        StartCoroutine(Timer());
    }

    public void StopTimer()
    {
        StopCoroutine(Timer());
    }

    public float GetTime()
    {
        return (time);
    }

    IEnumerator Timer()
    {
        while (true)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }

    public int GetResetCount()
    {
        return (resetCount);
    }

    public void SetResetCount(int x)
    {
        resetCount = x;
    }
}
