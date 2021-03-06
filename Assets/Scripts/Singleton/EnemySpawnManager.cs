﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum EnemyType { 
    none, // To create a break in the spawn wave
    ranged,
    melee,
    dragon
    };

public class EnemySpawnManager : MonoBehaviour
{
    bool hasBeenInitialized = false;

    // The path to the current wave file
    private string waveInfoFilePath;
    
    // The first index of Path is for the spawning node
    public PathNode[] Path;

    public GameObject ranged;
    public GameObject melee;
    public GameObject dragon;

    public float DistanceToEnd;

    private List<string> waveData;
    private Queue<EnemyType> spawnQueue;

    private float spawnDelta;
    private float spawnDeltaRemaining;

    private QueueHolder queueHolder;
    private int currentWaveNumber;
    private AudioSource audioSource;

    [SerializeField] private Button nextWaveButton;
    [SerializeField] private GameObject shop;

    private enum SpawnState{
        paused,
        spawning
    };

    private SpawnState spawnState;

    void Start()
    {

        //initialize any arrays or dictionaries

        waveData = new List<string>();
        spawnQueue = new Queue<EnemyType>();

        spawnState = SpawnState.paused;

        // Calculate distance to the end of the level from the spawn location of enemies
        DistanceToEnd = 0f;

        for (int i = 0; i < Path.Length - 1; i++)
        {
            DistanceToEnd += (Path[i + 1].transform.position - Path[i].transform.position).magnitude;
        }

        queueHolder = GetComponent<QueueHolder>();
        audioSource = GetComponent<AudioSource>();

        // Default value
        //print(Application.dataPath);
        ChangeWaveInfoFile("TestingSpawnFile");
        currentWaveNumber = 0;
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
    /// <param name="FileName">File name of the new .wif file</param>
    /// <returns>True if successful</returns>
    public bool ChangeWaveInfoFile(string FileName)
    {
        TextAsset LevelFile = Resources.Load<TextAsset>("LevelFiles/" + FileName);

        if (LevelFile.text != null)
        {
            waveData.Clear();
            waveData.AddRange(LevelFile.text.Split('\n'));
            waveInfoFilePath = "LevelFiles/" + FileName;
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Starts the next wave
    /// </summary>
    public void StartWave()
    {
        StartWave(currentWaveNumber + 1);
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
        else
        {
            for (int i = 0; i < queueHolder.objectQueue.Count; i++)
            {
                if (queueHolder.objectQueue[i].activeSelf)
                    return;
            }
        }
        
        currentWaveNumber = WaveNumber;

        if (WaveNumber <= waveData.Count) // Curated Mode
        {
            nextWaveButton.interactable = false;

            // Clear the spawnQueue
            spawnQueue.Clear();
            queueHolder.ClearAll();


            string[] currentWave = waveData[WaveNumber - 1].Split(' ');

            spawnDelta = float.Parse(currentWave[0]);
            spawnDeltaRemaining = spawnDelta;

            //print(spawnDelta);

            // Populate the spawnQueue
            for (int i = 1; i < currentWave.Length; i++)
            {
                spawnQueue.Enqueue((EnemyType)int.Parse(currentWave[i]));
            }

        }
        else // Win
        {
            SceneManager.LoadScene("Win Scene");
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
            for (int i = 0; i < queueHolder.objectQueue.Count; i++)
            {
                if (queueHolder.objectQueue[i].activeSelf)
                    return;
            }


            // Give end of round bonus gold and set variables to default values
            int endOfRoundBonus = (int)(20.0 + (float)currentWaveNumber * (((float)currentWaveNumber - 3.0) / 15.0 + 5.0));
            GameStats.Instance.AddGold(endOfRoundBonus);

            // Clear the spawnQueue
            spawnQueue.Clear();
            queueHolder.ClearAll();

            // Change the button back to interactable
            nextWaveButton.interactable = true;

            // Refresh shop offerings
            shop.GetComponent<Shop>().RefreshShop();

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
                enemy.GetComponent<Enemy>().manager = GetComponent<EnemySpawnManager>();
                if (spawnQueue.Count == 0)
                    enemy.GetComponent<Enemy>().Value += (int)(20.0 + (float)currentWaveNumber * (((float)currentWaveNumber - 3.0) / 15.0 + 5.0));
                queueHolder.objectQueue.Add(enemy);
                break;
            case EnemyType.ranged:
                enemy = Instantiate(ranged, Path[0].transform.position, Quaternion.identity);
                enemy.GetComponent<FollowPath>().DistanceToEnd = DistanceToEnd;
                enemy.GetComponent<FollowPath>().Path = Path;
                enemy.GetComponent<Enemy>().manager = GetComponent<EnemySpawnManager>();
                if (spawnQueue.Count == 0)
                    enemy.GetComponent<Enemy>().Value += (int)(20.0 + (float)currentWaveNumber * (((float)currentWaveNumber - 3.0) / 15.0 + 5.0));
                queueHolder.objectQueue.Add(enemy);
                break;
            case EnemyType.dragon:
                enemy = Instantiate(dragon, Path[0].transform.position, Quaternion.identity);
                enemy.GetComponent<FollowPath>().DistanceToEnd = DistanceToEnd;
                enemy.GetComponent<FollowPath>().Path = Path;
                enemy.GetComponent<Enemy>().manager = GetComponent<EnemySpawnManager>();
                if (spawnQueue.Count == 0)
                    enemy.GetComponent<Enemy>().Value += (int)(20.0 + (float)currentWaveNumber * (((float)currentWaveNumber - 3.0) / 15.0));
                queueHolder.objectQueue.Add(enemy);
                break;
            case EnemyType.none:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Plays an audio clip
    /// </summary>
    /// <param name="clip">The clip to play</param>
    /// <param name="volume">The relative volume to play it at</param>
    public void playSound(AudioClip clip, float volume)
    {
        audioSource.PlayOneShot(clip, volume);
    }

    public void playSoundNoOverlap(AudioClip clip, float volume)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }
}
