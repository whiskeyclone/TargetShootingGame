using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    [SerializeField] int ignoreCollisionLayer;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy instance if another instance exists
            return;
        }

        Physics2D.IgnoreLayerCollision(ignoreCollisionLayer, ignoreCollisionLayer); // Ignore collisions between objects on ignoreCollisionLayer
        AudioController.instance.PlaySound("Main Song");
    }

    private void Update()
    {
        
    }
}
