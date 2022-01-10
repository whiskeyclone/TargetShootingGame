using UnityEngine;

public class Bullet : MonoBehaviour
{
    float xDestroyBound = 10f;
    float yDestroyBound = 6f;

    // Destroy this object if it goes beyond xDestroyBound or yDestroyBound
    void CheckToDestroy()
    {
        if ((transform.position.x >= xDestroyBound) || (transform.position.x <= -xDestroyBound) || (transform.position.y >= yDestroyBound) || (transform.position.y <= -yDestroyBound))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        CheckToDestroy();
    }
}
