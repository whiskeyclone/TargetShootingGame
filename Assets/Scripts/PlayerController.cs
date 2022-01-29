using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] GameObject bullet;
    Physics physics;
    const float bulletSpeed = 10f;
    int moveYDirection = 0;
    const float moveSpeed = 5f;
    int ammo = 3;

    private void Awake()
    {
        // Set instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this); // Destroy instance if another instance exists
            return;
        }

        physics = GetComponent<Physics>();
    }

    void FireBullet()
    {
        // Get direction to fire in
        Vector2 fireDir = (Crosshair.instance.transform.position - transform.position).normalized;

        // Instantiate bullet
        GameObject bulletInst = Instantiate(bullet, transform.position, transform.rotation);
        bulletInst.GetComponent<Rigidbody2D>().velocity = fireDir * bulletSpeed;

        // Decrease ammo
        ammo--;
        AmmoUI.instance.DecreaseAmmoUI();
    }

    public int GetAmmo()
    {
        return (ammo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && (ammo > 0))
        {
            FireBullet();
        }

        // Get y direction for movement
        moveYDirection = 0;

        if ((Input.GetKey(KeyCode.W)) && (physics.collisionInfo.above == false))
        {
            moveYDirection++;
        }

        if ((Input.GetKey(KeyCode.S)) && (physics.collisionInfo.below == false))
        {
            moveYDirection--;
        }

        // Move
        physics.Move(new Vector2(0, moveYDirection * moveSpeed * Time.deltaTime));
    }
}
