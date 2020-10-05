using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public PathNode[] path;
    public float MoveSpeed;
    
    private int currentNode;
    private bool hasReachedLast;

    // Start is called before the first frame update
    void Start()
    {
        currentNode = 0;
        hasReachedLast = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasReachedLast)
        {
            MoveDirectly();
            CheckDistance();
        }
    }

    // Moves directly towards the next node (straight line)
    private void MoveDirectly()
    {
        // Calculate distance and angle to travel
        Vector3 direction = path[currentNode].transform.position - transform.position;
        direction.Normalize();

        // "Teleport" the object towards its destination by the movespeed / the time this frame took to process
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }

    // Changes the target node if the current node is reached
    private void CheckDistance()
    {
        if((path[currentNode].transform.position- transform.position).magnitude < 0.01f)
        {
            // Increase the index of the current node if possible
            if (currentNode != path.Length - 1)
                SetNode(currentNode + 1);
            else
            {
                // Stop moving if the current node is the last one
                hasReachedLast = true;
            }
        }
    }

    // Changes the current node
    public void SetNode(int node)
    {
        currentNode = node;
    }

}
