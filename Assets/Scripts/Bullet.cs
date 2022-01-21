using UnityEngine;

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
        // Destroy bullet when it goes off camera
        if ((transform.position.x >= cameraBounds.max.x + cameraBoundsDestroyOffset) || (transform.position.x <= cameraBounds.min.x - cameraBoundsDestroyOffset) || (transform.position.y >= cameraBounds.max.y + cameraBoundsDestroyOffset) || (transform.position.y <= cameraBounds.min.y - cameraBoundsDestroyOffset))
        {
            DestroySelf();
        }
    }

    // Destroy bullet and check to see if scene should be restarted
    public void DestroySelf()
    {
        int bulletCount = GameObject.FindGameObjectsWithTag("Bullet").Length;
        int targetCount = GameObject.FindGameObjectsWithTag("Target").Length;

        // If the player has no ammo, this is the last bullet in the scene, and there are still targets in the scene, restart scene
        if ((PlayerController.instance.GetAmmo() == 0) && (bulletCount == 1) && (targetCount > 0))
        {
            Controller.instance.RestartScene();
        }
        else
        {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.transform.tag == "Wall") || (collision.transform.tag == "Slope"))
        {
            bounceCount++;

            if (bounceCount > maxBounces)
            {
                DestroySelf();
            }
            else
            {
                // Redden bullet color
                Color currentColor = spriteRend.color;
                Color newColor = new Color(currentColor.r, Mathf.Clamp(currentColor.g - reddenAmount, 0, 1), currentColor.b);
                spriteRend.color = newColor;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target")
        {
            int targetCount = GameObject.FindGameObjectsWithTag("Target").Length;

            // Move to next level if level is complete
            if (targetCount == 1)
            {
                Controller.instance.GoToNextScene();
            }
            else
            {
                // Destroy target and bullet
                Destroy(collision.gameObject);
                DestroySelf();
            }
        }
    }
}
