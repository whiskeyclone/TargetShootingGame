using UnityEngine;

public class Bullet : MonoBehaviour
{
    Physics physics;
    Vector2 direction = Vector2.zero;
    float speed = 10f;

    private void Start()
    {
        physics = GetComponent<Physics>();
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    private void Update()
    {
        // Move
        physics.Move(direction * speed * Time.deltaTime);

        // Reverse direction on horizontal collision
        if (physics.collisionInfo.left || physics.collisionInfo.right)
        {
            direction = new Vector2(direction.x * -1, direction.y);
        }
    }
}
