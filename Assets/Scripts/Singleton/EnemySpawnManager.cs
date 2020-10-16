﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
public enum EnemyType { 
    none, // To create a break in the spawn wave
    ranged,
    melee };

public class EnemySpawnManager : Singleton<EnemySpawnManager>
{
    bool hasBeenInitialized = false;

    // The path to the current wave file
    private string waveInfoFilePath;
    
    // The first index of Path is for the spawning node
    public PathNode[] Path;

    public GameObject ranged;
    public GameObject melee;

    public float DistanceToEnd;

    private List<string> waveData;
    private Queue<EnemyType> spawnQueue;

    private float spawnDelta;
    private float spawnDeltaRemaining;

    QueueHolder queueHolder;

    private enum SpawnState{
        paused,
        spawning
    };

    private SpawnState spawnState;

    public override void Initialize()
    {
        //only initialize if we haven't been before
        if (hasBeenInitialized) return;
        //initialize any arrays or dictionaries in the Singleton

        waveData = new List<string>();
        spawnQueue = new Queue<EnemyType>();

        spawnState = SpawnState.paused;

        // Calculate distance to the end of the level from the spawn location of enemies
        DistanceToEnd = 0f;

        for (int i = 0; i < Path.Length - 1; i++)
        {
            DistanceToEnd += (Path[i + 1].transform.position - Path[i].transform.position).magnitude;
        }

        hasBeenInitialized = true;
        queueHolder = gameObject.GetComponent<QueueHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn enemies on spawning
        if(spawnState == SpawnState.spawning)
        {
            if (spawnDeltaRemaining <= 0)
            {
                SpawnEnemy();
                spawnDeltaRemaining += spawnDelta;
            }
            else
            {
                spawnDeltaRemaining -= Time.deltaTime;
            }
        }
        else
        {

        }
    }

    /// <summary>
    /// Changes the file that the wave is 
    /// </summary>
    /// <param name="FilePath">File Path of the new .wif file</param>
    /// <returns>True if successful</returns>
    public bool ChangeWaveInfoFile(string FilePath)
    {
        // Test to see if the file is the right format
        if(FilePath.Substring(FilePath.Length - 4) != ".wif")
        {
            //print("not suitable file structure");
            return false;
        }

        // Test to see if the file exists and has data
        try
        {
            using (StreamReader reader = new StreamReader(FilePath))
            {
                string test;

                if ((test = reader.ReadLine()) != null)
                {
                    print(test);
                    waveInfoFilePath = FilePath;
                    waveData.Clear();
                    waveData.Add(test);

                    waveData.AddRange(reader.ReadToEnd().Split('\n'));

                    return true;
                }
            }
                return false;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Starts the Next wave of Enemy Spawns
    /// </summary>
    /// <param name="WaveNumber">The wave number of the wave to start</param>
    public void StartWave(int WaveNumber)
    {
        // Don't let 2 waves spawn at the same time
        if (spawnState == SpawnState.spawning)
            return;

        if (WaveNumber < waveData.Count) // Curated Mode
        {
            // Clear the spawnQueue
            spawnQueue.Clear();
            queueHolder.objectQueue.Clear();


            string[] currentWave = waveData[WaveNumber - 1].Split(' ');

            spawnDelta = float.Parse(currentWave[0]);
            spawnDeltaRemaining = spawnDelta;

            print(spawnDelta);

            // Populate the spawnQueue
            for (int i = 1; i < currentWave.Length; i++)
            {
                spawnQueue.Enqueue((EnemyType)int.Parse(currentWave[i]));
            }

        }
        else // Endless mode
        {
            // TODO: Create algorithm for endless mode spawn determination
        }

        spawnState = SpawnState.spawning;
    }

    /// <summary>
    /// Spawns an enemy based off of the next type in the spawnQueue
    /// </summary>
    private void SpawnEnemy()
    {
        // Break once the spawn queue is empty
        if(spawnQueue.Count == 0)
        {
            spawnState = SpawnState.paused;
            return;
        }

        EnemyType enemyType = spawnQueue.Dequeue();
        GameObject enemy;

        switch (enemyType)
        {
            case EnemyType.melee:
                enemy = Instantiate(melee, Path[0].transform.position, Quaternion.identity);
                enemy.GetComponent<FollowPath>().DistanceToEnd = DistanceToEnd;
                enemy.GetComponent<FollowPath>().Path = Path;
                queueHolder.objectQueue.Add(enemy);
                break;
            case EnemyType.ranged:
                enemy = Instantiate(ranged, Path[0].transform.position, Quaternion.identity);
                enemy.GetComponent<FollowPath>().DistanceToEnd = DistanceToEnd;
                enemy.GetComponent<FollowPath>().Path = Path;
                queueHolder.objectQueue.Add(enemy);
                break;
            case EnemyType.none:
                break;
            default:
                break;
        }
    }
}