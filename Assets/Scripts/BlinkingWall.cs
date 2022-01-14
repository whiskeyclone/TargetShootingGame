using UnityEngine;

public class BlinkingWall : MonoBehaviour
{
    [SerializeField] int onState; // The state where this wall is active

    private void Start()
    {
        if ((onState < 1) || (onState > 2))
        {
            Debug.LogError("Invalid on state!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (BlinkTimer.instance.GetState() == onState)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
