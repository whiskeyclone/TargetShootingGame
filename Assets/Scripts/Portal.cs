using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal otherPortal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            // Teleport if bullet has touched no portals
            Bullet bullet = collision.GetComponent<Bullet>();

            if (bullet.GetPortalsTouchedWhileTeleporting() == 0)
            {
                collision.transform.position = otherPortal.transform.position;
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
