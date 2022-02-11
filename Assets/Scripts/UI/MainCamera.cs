using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static MainCamera instance;
    [SerializeField] GameObject wipe;
    [SerializeField] GameObject menuWipe;
    [SerializeField] Transform canvasTrans;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject ammoUI;
    Bounds cameraBounds;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

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

        if (Controller.instance.GetCurrentSceneIndex() == 0)
        {
            Instantiate(startMenu, canvasTrans);
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

    public void SpawnWipe()
    {
        if (GameObject.FindGameObjectsWithTag("Wipe").Length == 0) // If no wipes exist
        {
            Instantiate(wipe, canvasTrans);
        }
    }

    public void SpawnMenuWipe()
    {
        if (GameObject.FindGameObjectsWithTag("Wipe").Length == 0) // If no wipes exist
        {
            Instantiate(menuWipe, canvasTrans);
        }
    }

    // Destroy current menu
    public void DestroyMenu()
    {
        GameObject menu = GameObject.FindGameObjectWithTag("Menu");

        if (menu != null)
        {
            Destroy(menu);
        }
    }
}
