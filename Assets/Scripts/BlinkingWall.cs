using UnityEngine;

public class BlinkingWall : MonoBehaviour
{
    [SerializeField] int onState; // The state where this wall is active
    Vector3 scale;

    private void Start()
    {
        if ((onState < 1) || (onState > 2))
        {
            Debug.LogError("Invalid on state!");
        }

        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Enable/disable sprite and collider
        if (BlinkTimer.instance.GetState() == onState)
        {
            transform.localScale = scale;
        }
        else
        {
            transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
