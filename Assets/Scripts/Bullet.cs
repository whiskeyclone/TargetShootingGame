using UnityEngine;

public class Bullet : MonoBehaviour
{
    Physics physics;
    Vector2 direction = Vector2.zero;
    float speed = 10f;
    float xDestroyBound = 10f;
    float yDestroyBound = 6f;

    private void Start()
    {
        physics = GetComponent<Physics>();
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

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

        // Move
        physics.Move(direction * speed * Time.deltaTime);

        // Change direction on horizontal collision
        if (physics.collisionInfo.left || physics.collisionInfo.right)
        {
            if (physics.collisionInfo.collider != null)
            {
                if (physics.collisionInfo.collider.tag == "Wall")
                {
                    direction = new Vector2(direction.x * -1, direction.y);
                }
                else if (physics.collisionInfo.collider.tag == "Slope")
                {
                    direction = new Vector2(0, 1);
                }
            }         
        }
    }
}
