using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUpdateTest : MonoBehaviour
{
    // Start is called before the first frame update
    public int i = 0;
    private float timer = 0.0f;
    private float waitTime = (float)(1/60);

    void Start()
    {
        i = 2;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            timer = 0;
            Debug.Log(i);
            i++;
            if (i > 2000)
            {
                Debug.Log("Timescale hit");
                Time.timeScale = 0;
            }
        }
    }
}
