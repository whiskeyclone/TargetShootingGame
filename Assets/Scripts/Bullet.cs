using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    Bounds cameraBounds;
    float cameraBoundsDestroyOffset = 2f; // How far off the bounds of the camera the bullet has to go to be destroyed
    int bounceCount = 0;
    const int maxBounces = 3;

    private void Start()
    {
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
            // Get num of bullets in scene
            int bulletCount = GameObject.FindGameObjectsWithTag("Bullet").Length;

            // Restart scene if the player has no ammo and this is the only bullet on screen
            if ((PlayerController.instance.GetAmmo() == 0) && (bulletCount == 1))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.transform.tag == "Wall") || (collision.transform.tag == "Slope"))
        {
            bounceCount++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target")
        {
            // Get num of bullets in scene
            int bulletCount = GameObject.FindGameObjectsWithTag("Bullet").Length;
            int targetsLeft = GameObject.FindGameObjectsWithTag("Target").Length;

            // Restart scene if the player has no ammo, this is the only bullet on screen, and this is not the last target
            if ((PlayerController.instance.GetAmmo() == 0) && (bulletCount == 1) && (targetsLeft > 1))
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
