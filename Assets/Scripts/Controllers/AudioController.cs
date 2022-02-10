using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    [SerializeField] Sound[] sounds;

    float gameVolume = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy instance if another instance exists
            return;
        }

        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
            s.audioSource.loop = s.loop;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = gameVolume; // Set game volume
    }

    public void PlaySound(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                s.audioSource.Play();
                return;
            }
        }

        Debug.LogError("Sound: " + name + " not found!");
    }

    public void FadeInSound(string name, float fadeTime)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                StartCoroutine(FadeIn(s.audioSource, fadeTime));
                return;
            }
        }

        Debug.LogError("Sound: " + name + " not found!");
    }

    public void FadeOutSound(string name, float fadeTime)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                StartCoroutine(FadeOut(s.audioSource, fadeTime));
                return;
            }
        }

        Debug.LogError("Sound: " + name + " not found!");
    }

    IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
    {
        float targetVol = audioSource.volume;
        float currentTime = 0f;
        audioSource.Play();

        while (currentTime < fadeTime)
        {
            audioSource.volume = Mathf.Lerp(0, targetVol, currentTime / fadeTime);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVol = audioSource.volume;
        float currentTime = 0f;

        while (currentTime < fadeTime)
        {
            audioSource.volume = Mathf.Lerp(startVol, 0, currentTime / fadeTime);
            currentTime += Time.deltaTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVol;
    }

    public void StopSound(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                s.audioSource.Stop();
                return;
            }
        }

        Debug.LogError("Sound: " + name + " not found!");
    }

    public bool IsSoundPlaying(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                return (s.audioSource.isPlaying);
            }
        }

        Debug.LogError("Sound: " + name + " not found!");
        return (false);
    }
}