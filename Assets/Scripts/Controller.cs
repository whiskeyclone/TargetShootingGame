using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] int ignoreCollisionLayer;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(ignoreCollisionLayer, ignoreCollisionLayer);
    }
}
