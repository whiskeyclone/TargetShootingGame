using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal otherPortal;
    [SerializeField] Transform sprite1Trans;
    [SerializeField] Transform sprite2Trans;
    [SerializeField] GameObject explosion;

    float rotateSpeed = 40f;

    private void Update()
    {
        // Rotate sprites
        sprite1Trans.Rotate(0f, 0f, -rotateSpeed * Time.deltaTime);
        sprite2Trans.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    void CreateExplosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {       
            Bullet bullet = collision.GetComponent<Bullet>();
            ParticleSystem partSys = bullet.GetComponent<ParticleSystem>();

            if (bullet.GetPortalsTouchedWhileTeleporting() == 0) // Enter first portal
            {
                // Stop and clear bullet particles
                partSys.Stop();
                partSys.Clear();

                collision.transform.position = otherPortal.transform.position; // Teleport
                CreateExplosion();
            }
            else if (bullet.GetPortalsTouchedWhileTeleporting() == 1) // Enter second portal
            {
                partSys.Play(); // Play bullet particles
                CreateExplosion();
            }

            bullet.SetPortalsTouchedWhileTeleporting(bullet.GetPortalsTouchedWhileTeleporting() + 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            // If portal has touched 2 portals, set portals touched to 0
            Bullet bullet = collision.GetComponent<Bullet>();

            if (bullet.GetPortalsTouchedWhileTeleporting() == 2)
            {
                bullet.SetPortalsTouchedWhileTeleporting(0);
            }
        }
    }
}
