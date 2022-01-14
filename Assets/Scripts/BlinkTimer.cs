using UnityEngine;

public class BlinkTimer : MonoBehaviour
{
    public static BlinkTimer instance;

    [SerializeField] float blinkSpeed;
    int state = 1;
    float counter = 0;

    private void Start()
    {
        // Set instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this); // Destroy instance if another instance exists
            return;
        }

        if (blinkSpeed <= 0)
        {
            Debug.LogError("Invalid blink speed!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Switch currentState between 1 and 2 based on blinkSpeed
        if (counter < blinkSpeed)
        {
            counter += Time.deltaTime;
        }
        else
        {
            if (state == 1)
            {
                state = 2;
            }
            else if (state == 2)
            {
                state = 1;
            }

            counter = 0;
        }
    }

    public int GetState()
    {
        return (state);
    }
}
