using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static MainCamera instance;
    Bounds cameraBounds;

    // Start is called before the first frame update
    void Start()
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

        InitializeCameraBounds();
    }

    void InitializeCameraBounds()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.main.orthographicSize * 2;
        cameraBounds = new Bounds(Camera.main.transform.position, new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
    }

    public Bounds GetCameraBounds()
    {
        return (cameraBounds);
    }
}
