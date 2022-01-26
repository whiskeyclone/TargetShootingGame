// Object with path node children is first child, object to move is second child

using UnityEngine;

public class Path : MonoBehaviour
{
    Vector3[] nodePositions;

    int targetPositionIndex = 1;
    [SerializeField] float moveSpeed = 5f;
    Transform moveObject;

    // Start is called before the first frame update
    void Start()
    {
        // Get node positions
        PathNode[] nodes = transform.GetChild(0).GetComponentsInChildren<PathNode>();
        nodePositions = new Vector3[nodes.Length];

        for (int i = 0; i < nodes.Length; i++)
        {
            nodePositions[i] = nodes[i].transform.position;
        }

        // Get object to be moved
        moveObject = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        if ((moveSpeed > 0) && (moveObject != null))
        {
            // Move parent
            moveObject.position = Vector3.MoveTowards(moveObject.position, nodePositions[targetPositionIndex], moveSpeed * Time.deltaTime);

            // Update target node
            if (moveObject.position == nodePositions[targetPositionIndex])
            {
                if (targetPositionIndex + 1 > nodePositions.Length - 1)
                {
                    targetPositionIndex = 0;
                }
                else
                {
                    targetPositionIndex++;
                }
            }
        }
    }
}
