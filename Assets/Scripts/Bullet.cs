using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    SpriteRenderer spriteRend;
    Bounds cameraBounds;
    float cameraBoundsDestroyOffset = 2f; // How far off the bounds of the camera the bullet has to go to be destroyed
    int bounceCount = 0;
    const int maxBounces = 3;
    const float reddenAmount = 0.2f; // How much to redden the bullet sprite when bouncing
    int portalsTouchedWhileTeleporting = 0;

    private void Start()
    {
        // Get components
        spriteRend = GetComponent<SpriteRenderer>();

        // Get camera bounds
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.main.orthographicSize * 2;
        cameraBounds = new Bounds(Camera.main.transform.position, new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
    }

    private void Update()
    {
        // Check to destroy bullet
        if ((bounceCount > maxBounces) || (transform.position.x >= cameraBounds.max.x + cameraBoundsDestroyOffset) || (transform.position.x <= cameraBounds.min.x - cameraBoundsDestroyOffset) || (transform.position.y >= cameraBounds.max.y + cameraBoundsDestroyOffset) || (transform.position.y <= cameraBounds.min.y - cameraBoundsDestroyOffset))
        {
            // Restart scene if the player has no ammo and this is the only bullet on screen
            if (IsLastBullet() == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            Destroy(gameObject);
        }
    }

    public int GetPortalsTouchedWhileTeleporting()
    {
        return (portalsTouchedWhileTeleporting);
    }

    public void SetPortalsTouchedWhileTeleporting(int x)
    {
        if (x <= 2)
        {
            portalsTouchedWhileTeleporting = x;
        }
        else
        {
            Debug.LogError("Bullet cannot touch more than 2 portals!");
        }
    }

    // Returns true if this bullet is the only one in the scene and the player has no ammo
    bool IsLastBullet()
    {
        int bulletCount = GameObject.FindGameObjectsWithTag("Bullet").Length;

        if ((PlayerController.instance.GetAmmo() == 0) && (bulletCount == 1))
        {
            return (true);
        }
        else
        {
            return (false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.transform.tag == "Wall") || (collision.transform.tag == "Slope"))
        {
            bounceCount++;

            // Redden color
            Color currentColor = spriteRend.color;
            Color newColor = new Color(currentColor.r, Mathf.Clamp(currentColor.g - reddenAmount, 0, 1), currentColor.b);
            spriteRend.color = newColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target")
        {
            int targetsLeft = GameObject.FindGameObjectsWithTag("Target").Length;

            // Restart scene if the player has no ammo, this is the only bullet on screen, and this is not the last target
            if ((IsLastBullet() == true) && (targetsLeft > 1))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                // Destroy target and bullet, decrement targetsLeft
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
