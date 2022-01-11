using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    const float xDestroyBound = 30f;
    const float yDestroyBound = 30f;
    int bounceCount = 0;
    const int maxBounces = 3;

    private void Update()
    {
        // Destroy this object if it goes beyond xDestroyBound or yDestroyBound
        if ((transform.position.x >= xDestroyBound) || (transform.position.x <= -xDestroyBound) || (transform.position.y >= yDestroyBound) || (transform.position.y <= -yDestroyBound))
        {
            Destroy(gameObject);
        }

        if (bounceCount > maxBounces)
        {
            // Restart scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            // Destroy target and bullet, decrement targetsLeft
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Controller.instance.DecrementTargetsLeft();
        }
    }
}
