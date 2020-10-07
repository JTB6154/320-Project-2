using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Replace with the game manager
public class QueueHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public Queue<GameObject> objectQueue;
    void Start()
    {
        this.objectQueue= new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
