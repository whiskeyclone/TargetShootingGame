using UnityEngine;

public class PortalLineDrawer : MonoBehaviour
{
    LineRenderer lineRend;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        lineRend = GetComponent<LineRenderer>();
        
        // Draw line
        for (int i = 0; i < 2; i++)
        {
            lineRend.SetPosition(i, transform.GetChild(i).transform.position);
        }
    }
}
