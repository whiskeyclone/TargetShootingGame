using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    float bulletSpeed = 5f;

    void FireBullet()
    {
        // Get direction to fire in
        Vector2 fireDir = (Crosshair.instance.transform.position - transform.position).normalized;

        // Instantiate bullet
        Rigidbody2D bulletInstRb = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
        bulletInstRb.velocity = (fireDir * bulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireBullet();
        }
    }
}
