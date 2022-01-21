using UnityEngine;
using System.Collections.Generic;

public class BlinkingWall : MonoBehaviour
{
    [SerializeField] int onState; // The state where this wall is active
    [SerializeField] Collider2D col;
    [SerializeField] SpriteRenderer spriteRend;
    [SerializeField] Collider2D bulletCheckCol;
    bool active = false;

    private void Start()
    {
        // Check if onState is valid
        if ((onState < 1) || (onState > 2))
        {
            Debug.LogError("Invalid on state!");
        }

        // Disable sprite and collider
        col.enabled = false;
        spriteRend.enabled = false;
    }

    void Update()
    {
        // Blink
        if (active == false)
        {
            if (BlinkTimer.instance.GetState() == onState)
            {
                CheckForBullets();

                // Enable sprite and collider
                col.enabled = true;
                spriteRend.enabled = true;
                active = true;
            }
        }
        else
        {
            if (BlinkTimer.instance.GetState() != onState)
            {
                // Disable sprite and collider
                col.enabled = false;
                spriteRend.enabled = false;
                active = false;
            }
        }
    }

    // Checks bulletCheckCollider for overlapping bullets and destroys them
    void CheckForBullets()
    {
        // Get overlapping colliders
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();

        bulletCheckCol.OverlapCollider(contactFilter.NoFilter(), results);

        // Destroy bullets
        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].tag == "Bullet")
            {
                results[i].GetComponent<Bullet>().DestroySelf();
            }
        }
    }
}
