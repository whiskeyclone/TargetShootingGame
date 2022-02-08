using UnityEngine;

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