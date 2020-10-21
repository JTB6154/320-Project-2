using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject target;
    public float Damage { get; set; }

    private float nextActionTime = 0.0f;
    public float period = 0.016f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 scale = new Vector3(1.0f, 1.0f, 1f);
        transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            if (target != null && target.GetComponent<Enemy>().CurrentHealth > 0)
            {
                this.transform.position += (0.05f * (target.transform.position - this.transform.position));
                if (target.GetComponent<Enemy>().CurrentHealth > 0)
                {
                    if (AABBCollision(gameObject, target))
                    {
                        target.GetComponent<Enemy>().TakeDamage(Damage);
                        Destroy(gameObject);
                    }
                    if (target.GetComponent<Enemy>().CurrentHealth < 0 || target.activeInHierarchy == false || target == null)
                    {
                        Destroy(gameObject);
                    }
                }

            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetTarget(GameObject target, float damage)
    {
        this.target = target;
        this.Damage = damage;
    }

    public bool AABBCollision(GameObject gameObject1, GameObject gameObject2)
    {
        //Returns true if there is a collision happening
        SpriteRenderer collider1 = gameObject1.GetComponent<SpriteRenderer>();
        SpriteRenderer collider2 = gameObject2.GetComponent<SpriteRenderer>();
        if (collider2.bounds.min.x < collider1.bounds.max.x)
        {
            if (collider2.bounds.max.x > collider1.bounds.min.x)
            {
                if (collider2.bounds.max.y > collider1.bounds.min.y)
                {
                    if (collider2.bounds.min.y < collider1.bounds.max.y)
                    {
                        //Debug.Log("AABB");
                        return true;
                    }
                }
            }
        }
        else
        {
            return false;
        }
        return false;
    }
}
