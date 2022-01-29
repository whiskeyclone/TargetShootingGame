using UnityEngine;

public class Bullet : MonoBehaviour
{
    SpriteRenderer spriteRend;
    ParticleSystem particleSys;
    [SerializeField] GameObject explosion;
    Bounds cameraBounds;
    int bounceCount = 0;
    const int maxBounces = 3;
    const float reddenAmount = 0.2f; // How much to redden the bullet sprite when bouncing
    int portalsTouchedWhileTeleporting = 0;
    bool targetHit = false;

    private void Start()
    {
        // Get components
        spriteRend = GetComponent<SpriteRenderer>();
        particleSys = GetComponent<ParticleSystem>();

        // Get camera bounds
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.main.orthographicSize * 2;
        cameraBounds = new Bounds(Camera.main.transform.position, new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
    }

    private void Update()
    {
        // Destroy bullet when it goes off camera
        if ((transform.position.x >= cameraBounds.max.x) || (transform.position.x <= cameraBounds.min.x) || (transform.position.y >= cameraBounds.max.y) || (transform.position.y <= cameraBounds.min.y))
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        CreateExplosion();
        Destroy(gameObject);
    }

    public int GetPortalsTouchedWhileTeleporting()
    {
        return (portalsTouchedWhileTeleporting);
    }

    public void SetPortalsTouchedWhileTeleporting(int x)
    {
        if (x <= 2)
        {
            portalsTouchedWhileTeleporting = x;
        }
        else
        {
            Debug.LogError("Bullet cannot touch more than 2 portals!");
        }
    }

    // Redden bullet and trail color
    void ChangeColor()
    {
        // Get reddened color
        Color currentColor = spriteRend.color;
        Color newColor = new Color(currentColor.r, Mathf.Clamp(currentColor.g - reddenAmount, 0, 1), currentColor.b);

        // Apply color to bullet
        spriteRend.color = newColor;

        // Apply color to trail
        var main = particleSys.main;
        main.startColor = newColor;
    }

    // Spawn explosion object at this bullet's location and color it the current color of the bullet
    void CreateExplosion()
    {
        // Get color of bullet
        Color bulletColor = spriteRend.color;

        // Spawn explosion
        GameObject explosionInst = Instantiate(explosion, transform.position, transform.rotation);

        // Change explosion color
        var main = explosionInst.GetComponent<ParticleSystem>().main;
        main.startColor = bulletColor;

        // Set targetHit for explosion
        explosionInst.GetComponent<Explosion>().SetTargetHit(targetHit);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.transform.tag == "Wall") || (collision.transform.tag == "Slope"))
        {
            bounceCount++;

            if (bounceCount > maxBounces)
            {
                DestroySelf();
            }
            else
            {
                ChangeColor();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target")
        {
            targetHit = true;
            Destroy(collision.gameObject);
            DestroySelf();
        }
    }
}
