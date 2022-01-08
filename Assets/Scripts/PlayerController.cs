using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    void FireBullet()
    {
        // Get direction to fire in
        Vector2 fireDir = (Crosshair.instance.transform.position - transform.position).normalized;

        // Instantiate bullet
        GameObject bulletInst = Instantiate(bullet, transform.position, transform.rotation);
        bulletInst.GetComponent<Bullet>().SetDirection(fireDir);
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
