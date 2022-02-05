using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public static Crosshair instance;

    private void Start()
    {
        // Set instance
        if (instance == null) // If no other instance exists
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Make cursor invisible
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Follow mouse
        Vector3 mousePosition= Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        transform.position = mousePosition;
    }
}
