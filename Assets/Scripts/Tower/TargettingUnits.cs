using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettingUnits : MonoBehaviour
{
    public Queue<GameObject> gameObjectsQueue;
    public GameObject objectBeingShot;
    public GameObject arrayHolder;
    public float range;
    public bool shooting=false;
    public SpriteRenderer selfRenderer;
    public SpriteRenderer enemySprite;
    // Start is called before the first frame update
    void Start()
    {
        gameObjectsQueue=arrayHolder.GetComponent<QueueHolder>().objectQueue;
    }

    // Update is called once per frame
    void Update()
    {
        //Update queue every frame 
        gameObjectsQueue=arrayHolder.GetComponent<QueueHolder>().objectQueue;

        if(objectBeingShot!=null)
        {
            //Check if the last object being shot is still in range
            selfRenderer = this.GetComponent<SpriteRenderer>();
            enemySprite = objectBeingShot.GetComponent<SpriteRenderer>();
            float centerDistance = Mathf.Pow(Mathf.Pow(enemySprite.bounds.center.x - selfRenderer.bounds.center.x, 2f) +
                Mathf.Pow(enemySprite.bounds.center.y - selfRenderer.bounds.center.y, 2f), 0.5f);
            if(centerDistance<range || objectBeingShot.GetComponent<Enemy>().isActiveAndEnabled)
            {
                
            }
            else{
            foreach(GameObject x in gameObjectsQueue)
            {
                shooting=false;
                enemySprite = x.GetComponent<SpriteRenderer>();
                float newCenterDistance = Mathf.Pow(Mathf.Pow(enemySprite.bounds.center.x - selfRenderer.bounds.center.x, 2f) +
                    Mathf.Pow(enemySprite.bounds.center.y - selfRenderer.bounds.center.y, 2f), 0.5f);
                if(newCenterDistance<range || x.GetComponent<Enemy>().isActiveAndEnabled) //Change to is alive later
                {
                    objectBeingShot= x;
                    shooting=true;
                    break;
                }
            }
            }
        }
        else{
            //Search for the unit that is alive and towards the top of the queue and in range 
            foreach(GameObject x in gameObjectsQueue)
            {
                shooting=false;
                selfRenderer = this.GetComponent<SpriteRenderer>();
                enemySprite = x.GetComponent<SpriteRenderer>();
                float centerDistance = Mathf.Pow(Mathf.Pow(enemySprite.bounds.center.x - selfRenderer.bounds.center.x, 2f) +
                    Mathf.Pow(enemySprite.bounds.center.y - selfRenderer.bounds.center.y, 2f), 0.5f);
                
                //Check if the unit at the top of the queue is in range and alive 

                if(centerDistance<range || x.GetComponent<Enemy>().isActiveAndEnabled) //Change to is alive later
                {
                    objectBeingShot= x;
                    shooting=true;
                    break;
                }
            }
        }
    }
}
