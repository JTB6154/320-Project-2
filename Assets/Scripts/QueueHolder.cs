﻿using System.Collections;
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
}
