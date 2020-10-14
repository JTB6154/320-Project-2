using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesTest : MonoBehaviour
{
    public enum EnemyType { ranged, melee };

    // The first index of Path is for the spawning node
    public PathNode[] Path;

    public GameObject ranged;
    public GameObject melee;

    private float DistanceToEnd;

    // Testing variables
    private float timeToSpawn = 1f;
    private float timeLeft = 1f;

    // Start is called before the first frame update
    void Start()
    {
        EnemySpawnManager.Instance.Initialize();
        print(EnemySpawnManager.Instance.ChangeWaveInfoFile("C:/Users/Jack/Documents/GitHub/320-Project-2/Assets/TestingSpawnFile.wif"));
            EnemySpawnManager.Instance.StartWave(1);
    }

    // Update is called once per frame
    void Update()
    {
        /*// Timer to spawn an enemy every 10 seconds
        timeLeft -= Time.deltaTime;

        if(timeLeft <= 0f)
        {
            SpawnEnemy((EnemyType)Random.Range(0, 2));
            timeLeft = timeToSpawn;
        }*/
    }

    /// <summary>
    /// Spawns an enemy of a specified type
    /// </summary>
    /// <param name="enemyType">The type of enemy to spawn</param>
    void SpawnEnemy(EnemyType enemyType)
    {
        GameObject enemy;

        if (enemyType == EnemyType.melee)
        {
            enemy = Instantiate(melee, Path[0].transform.position, Quaternion.identity);
        }
        else if (enemyType == EnemyType.ranged)
        {
            enemy = Instantiate(ranged, Path[0].transform.position, Quaternion.identity);
        }
        else
        {
            enemy = Instantiate(melee, Path[0].transform.position, Quaternion.identity);
        }

        enemy.GetComponent<FollowPath>().DistanceToEnd = DistanceToEnd;
        enemy.GetComponent<FollowPath>().Path = Path;
    }
}
