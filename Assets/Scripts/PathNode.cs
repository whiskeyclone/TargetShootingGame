using UnityEngine;

public class PathNode : MonoBehaviour
{
    SpriteRenderer spriteRend;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.enabled = false;
    }
}
