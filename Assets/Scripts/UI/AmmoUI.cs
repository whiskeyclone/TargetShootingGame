using UnityEngine;
using System.Collections.Generic;

public class AmmoUI : MonoBehaviour
{
    public static AmmoUI instance;

    [SerializeField] GameObject bulletImage;
    List<GameObject> bulletImages;
    float spacing = 1f; // Distance between bullet images

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

        bulletImages = new List<GameObject>();
        InitializeUI();
    }

    void InitializeUI()
    {
        int ammoCount = PlayerController.instance.GetMaxAmmo();

        for (int i = 0; i < ammoCount; i++)
        {
            Vector2 position = new Vector2(transform.position.x + i * spacing, transform.position.y);
            bulletImages.Add(Instantiate(bulletImage, position, transform.rotation, transform));
        }
    }

    public void DecreaseUI()
    {
        if (bulletImages.Count > 0)
        {
            Destroy(bulletImages[bulletImages.Count - 1]);
            bulletImages.RemoveAt(bulletImages.Count - 1);
        }
        else
        {
            Debug.LogError("Cannot decrease ammo UI further!");
        }      
    }
}
