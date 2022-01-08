using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVelocity(Vector2 vel)
    {
        rb.velocity = vel;
    }
}
