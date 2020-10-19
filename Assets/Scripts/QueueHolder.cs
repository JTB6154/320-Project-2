using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Replace with the game manager
public class QueueHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> objectQueue;
    void Start()
    {
        this.objectQueue= new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Destroys game objects and clears the queue completely
    /// </summary>
    void ClearAll()
    {
        // Destroy objects
        while(objectQueue.Count > 0)
        {
            GameObject current = objectQueue[0];
            objectQueue.RemoveAt(0);
            Destroy(current);
        }

        // Clear the queue in case something didn't delete properly
        objectQueue.Clear();
    }
}
