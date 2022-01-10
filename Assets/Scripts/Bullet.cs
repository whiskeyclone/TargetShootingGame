using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    float xDestroyBound = 10f;
    float yDestroyBound = 6f;
    int bounceCount = 0;

    void CheckToDestroy()
    {
        // Destroy this object if it goes beyond xDestroyBound or yDestroyBound
        if ((transform.position.x >= xDestroyBound) || (transform.position.x <= -xDestroyBound) || (transform.position.y >= yDestroyBound) || (transform.position.y <= -yDestroyBound))
        {
            Destroy(gameObject);
        }

        if (bounceCount >= 4)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        CheckToDestroy();
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
