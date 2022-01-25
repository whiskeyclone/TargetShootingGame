using UnityEngine;

public class PortalLineDrawer : MonoBehaviour
{
    LineRenderer lineRend;
    [SerializeField] Transform portal1Trans;
    [SerializeField] Transform portal2Trans;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        lineRend = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineRend.SetPosition(0, portal1Trans.position);
        lineRend.SetPosition(1, portal2Trans.position);
    }
}
