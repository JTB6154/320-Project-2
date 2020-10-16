using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public PathNode[] Path;
    public float MoveSpeed;

    // Needs to be initialized when instantiated
    public float DistanceToEnd;
    public bool IsRanged;
    
    private int currentNode;
    private bool hasReachedLast;

    private float attackDeltaRemaining;
    // Start is called before the first frame update
    void Start()
    {
        currentNode = 0;
        hasReachedLast = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasReachedLast) // Movement
        {
            MoveDirectly();
            CheckDistance();
        }
        else // Attacking
        {
            if (attackDeltaRemaining <= 0)
            {
                gameObject.GetComponent<Enemy>().Attack();
                attackDeltaRemaining += gameObject.GetComponent<Enemy>().AttackSpeed;
            }
            else
            {
                attackDeltaRemaining -= Time.deltaTime;
            }
           
        }
    }

    // Moves directly towards the next node (straight line)
    private void MoveDirectly()
    {
        // Calculate distance and angle to travel
        Vector3 direction = Path[currentNode].transform.position - transform.position;
        direction.Normalize();

        // "Teleport" the object towards its destination by the movespeed / the time this frame took to process
        direction = direction * MoveSpeed * Time.deltaTime;
        transform.Translate(direction);

        // Update the distance to the end node
        DistanceToEnd -= direction.magnitude;
    }

    // Changes the target node if the current node is reached
    private void CheckDistance()
    {
        if (IsRanged)
        {
            if (currentNode == Path.Length - 1)
            {
                if ((Path[currentNode].transform.position - transform.position).magnitude < 1f)
                {
                    hasReachedLast = true;
                }
            }
            else if ((Path[currentNode].transform.position - transform.position).magnitude < 0.01f && currentNode != Path.Length - 1)
            {
                // Increase the index of the current node if possible
                SetNode(currentNode + 1);
            }
        }
        else if ((Path[currentNode].transform.position - transform.position).magnitude < 0.01f)
        {
            // Increase the index of the current node if possible
            if (currentNode != Path.Length - 1)
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
