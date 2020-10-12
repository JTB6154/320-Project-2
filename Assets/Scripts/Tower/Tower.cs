using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tower : MonoBehaviour
{
    TowerData data;
    public bool isUnitAssigned;
    public int Tier = 1;
    public GameObject gameManager;
    public float timer = 0.0f;
    public float waitTime = 0.0f;

    private List<GameObject> gameObjectsQueue;
    private GameObject target;
    private bool shooting = false;
    private SpriteRenderer selfRenderer;
    private SpriteRenderer enemySprite;

    void Start()
    {
        gameObjectsQueue=new List<GameObject>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        waitTime = 1000 / AttackSpeed;
        if (timer > waitTime)
        {
            timer -= waitTime;
            FindAndShootTarget();
        }
    }

    public float AttackSpeed
    {
        get
        {
            return data.AttackSpeed[Tier - 1];
        }
    }
    public float Damage
    {
        get
        {
            return data.Damage[Tier - 1];
        }
    }
    public float Range
    {
        get
        {
            return data.Range[Tier - 1];
        }
    }

    public void SetData(TowerData newData)
    {
        if (data != null) return;

        data = newData;
    }

    public void TierUp()
    {
        if(Tier < 3)
        {
            Tier++;
        }
    }

    public void AssignUnit(GameObject gameObject)
    {
        isUnitAssigned = true;
        SetData(gameObject.GetComponent<TroopPlaceholder>().data);
    }

    public void FindAndShootTarget()
    {
        //Update queue every frame 
        gameObjectsQueue = gameManager.GetComponent<QueueHolder>().objectQueue;
        gameObjectsQueue = gameObjectsQueue.OrderBy(x => x.GetComponent<EnemySpawnManager>().DistanceToEnd).ToList();
        if (target != null)
        {
            //Check if the last object being shot is still in range
            selfRenderer = this.GetComponent<SpriteRenderer>();
            enemySprite = target.GetComponent<SpriteRenderer>();
            float centerDistance = Mathf.Pow(Mathf.Pow(enemySprite.bounds.center.x - selfRenderer.bounds.center.x, 2f) +
                Mathf.Pow(enemySprite.bounds.center.y - selfRenderer.bounds.center.y, 2f), 0.5f);
            if (centerDistance < this.Range && target.GetComponent<Enemy>().CurrentHealth > 0)
            {

            }
            else
            {
                foreach (GameObject x in gameObjectsQueue)
                {
                    shooting = false;
                    enemySprite = x.GetComponent<SpriteRenderer>();
                    float newCenterDistance = Mathf.Pow(Mathf.Pow(enemySprite.bounds.center.x - selfRenderer.bounds.center.x, 2f) +
                        Mathf.Pow(enemySprite.bounds.center.y - selfRenderer.bounds.center.y, 2f), 0.5f);
                    if (newCenterDistance < this.Range && x.GetComponent<Enemy>().CurrentHealth>0) //Change to is alive later
                    {
                        target = x;
                        shooting = true;
                        break;
                    }
                }
            }
        }
        else
        {
            //Search for the unit that is alive and towards the top of the queue and in range 
            foreach (GameObject x in gameObjectsQueue)
            {
                shooting = false;
                selfRenderer = this.GetComponent<SpriteRenderer>();
                enemySprite = x.GetComponent<SpriteRenderer>();
                float centerDistance = Mathf.Pow(Mathf.Pow(enemySprite.bounds.center.x - selfRenderer.bounds.center.x, 2f) +
                    Mathf.Pow(enemySprite.bounds.center.y - selfRenderer.bounds.center.y, 2f), 0.5f);

                //Check if the unit at the top of the queue is in range and alive 

                if (centerDistance < this.Range && x.GetComponent<Enemy>().CurrentHealth > 0) //Change to is alive later
                {
                    target = x;
                    shooting = true;
                    break;
                }
            }
        }
        if (shooting == true && target != null)
        {
            target.GetComponent<Enemy>().TakeDamage(this.Damage);
        }
    }
}
