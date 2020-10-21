using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tower : MonoBehaviour
{
    TroopPlaceholder troop;
    public bool isUnitAssigned;
    public GameObject enemyQueueHolder;
    public QueueHolder enemyQueue;
    public List<GameObject> projectiles;
    public GameObject projectile;
    public GameObject towerManager;

    public float timer = 0.0f;
    public float waitTime = 0.0f;

    private List<GameObject> gameObjectsQueue;
    private GameObject target;
    private bool shooting = false;
    private SpriteRenderer selfRenderer;
    Sprite emptyTower;
    private SpriteRenderer enemySprite;

    void Start()
    {
        towerManager = GameObject.Find("TowerManager");
        projectile = towerManager.GetComponent<TowerManager>().projectile;
        enemyQueue = enemyQueueHolder.GetComponent<QueueHolder>();
        gameObjectsQueue = new List<GameObject>();
        selfRenderer = GetComponent<SpriteRenderer>();
        emptyTower = selfRenderer.sprite;
    }

    void Update()
    {
        projectile = towerManager.GetComponent<TowerManager>().projectile;
        if (!isUnitAssigned) return;
        timer += Time.deltaTime;
        waitTime = 1 / troop.AttackSpeed;
        if (timer > waitTime)
        {
            timer -= waitTime;
            FindAndShootTarget();
        }

    }

    public void AssignUnit(TroopPlaceholder troop)
    {
        if (isUnitAssigned) return;
        isUnitAssigned = true;
        this.troop = troop;
        selfRenderer.sprite = troop.BattlefieldSprite;
    }

    public TroopPlaceholder RemoveUnit()
    {
        if (!isUnitAssigned) return null;
        isUnitAssigned = false;
        TroopPlaceholder temp = troop;
        troop = null;
        selfRenderer.sprite = emptyTower;
        return temp;

    }

    public void FindAndShootTarget()
    {
        //Update queue every frame 
        gameObjectsQueue = enemyQueue.objectQueue;
        gameObjectsQueue = gameObjectsQueue.OrderBy(x => x.GetComponent<FollowPath>().DistanceToEnd).ToList();
        if (target != null)
        {
            //Check if the last object being shot is still in range
            selfRenderer = this.GetComponent<SpriteRenderer>();
            enemySprite = target.GetComponent<SpriteRenderer>();
            float centerDistance = Mathf.Pow(Mathf.Pow(enemySprite.bounds.center.x - selfRenderer.bounds.center.x, 2f) +
                Mathf.Pow(enemySprite.bounds.center.y - selfRenderer.bounds.center.y, 2f), 0.5f);
            if (centerDistance < troop.Range && target.GetComponent<Enemy>().CurrentHealth > 0)
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
                    if (newCenterDistance < troop.Range && x.GetComponent<Enemy>().CurrentHealth > 0) //Change to is alive later
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

                if (centerDistance < troop.Range && x.GetComponent<Enemy>().CurrentHealth > 0) //Change to is alive later
                {
                    target = x;
                    shooting = true;
                    break;
                }
            }
        }
        if (shooting == true && target != null)
        {
            projectile = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
            Debug.Log(projectile);
            projectile.GetComponent<Projectile>().SetTarget(target, troop.Damage);
            projectiles.Add(projectile);
        }
    }

}
