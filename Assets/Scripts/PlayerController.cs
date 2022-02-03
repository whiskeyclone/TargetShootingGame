using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform spriteTrans;

    Physics physics;
    const float bulletSpeed = 10f;
    int moveYDirection = 0;
    const float moveSpeed = 5f;
    int ammo = 4;
    float bulletSpawnOffset = 0.75f; // How far away from player to spawn bullet

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
        Vector2 fireDir = (Crosshair.instance.transform.position - transform.position).normalized; // Get direction to crosshair
        Vector2 bulletSpawnPos = new Vector2(transform.position.x, transform.position.y) + (fireDir * bulletSpawnOffset);

        // Instantiate bullet
        GameObject bulletInst = Instantiate(bullet, bulletSpawnPos, transform.rotation);
        bulletInst.GetComponent<Rigidbody2D>().velocity = fireDir * bulletSpeed;

        // Decrease ammo
        ammo--;
        AmmoUI.instance.DecreaseAmmoUI();
    }

    public int GetAmmo()
    {
        return (ammo);
    }

    void PointToCursor()
    {
        // Get direction to crosshair
        Vector2 aimDir = (Crosshair.instance.transform.position - transform.position).normalized;

        // Rotate to point towards crosshair
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90f;
        spriteTrans.eulerAngles = new Vector3(0, 0, angle);
    }

    void GetInputs()
    {
        // Restart scene
        if (Input.GetKeyDown(KeyCode.R))
        {
            Controller.instance.RestartScene();
        }

        // Fire bullet
        if (Input.GetKeyDown(KeyCode.Mouse0) && (ammo > 0))
        {
            FireBullet();
        }

        // Get y direction for movement
        moveYDirection = 0;

        // Move up
        if ((Input.GetKey(KeyCode.W)) && (physics.collisionInfo.above == false))
        {
            moveYDirection++;
        }

        // Move down
        if ((Input.GetKey(KeyCode.S)) && (physics.collisionInfo.below == false))
        {
            moveYDirection--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PointToCursor();
        GetInputs();
        
        physics.Move(new Vector2(0, moveYDirection * moveSpeed * Time.deltaTime)); // Move
    }
}
