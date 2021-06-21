using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectileScript : MonoBehaviour
{
    Transform target;
    TowerProjectile selfProjectile;
    public Tower selfTower;
    public int damage;
    GameControllerScript gcs;

    public void Start()
    {
        gcs = FindObjectOfType<GameControllerScript>();

        selfProjectile = gcs.AllProjectiles[selfTower.type];

        GetComponent<SpriteRenderer>().sprite = selfProjectile.Spr;
    }
    private void Update()
    {
        Move();
    }
    public void SetTarget(Transform enemy)
    {
        target = enemy;
    }

    private void Move()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) < 0.1f)
            {
                Hit();
            }
            else
            {
                Vector2 dir = target.position - transform.position;
                transform.Translate(dir.normalized * Time.deltaTime * selfProjectile.speed);
            }
        }
        else Destroy(gameObject);
    }

    void Hit()
    {
        switch(selfTower.type)
        {
            case (int)TowerType.First_tower:
                target.GetComponent<EnemyScript>().TakeDamage(damage);
                break;

            case (int)TowerType.Second_tower:
                target.GetComponent<EnemyScript>().TakeDamage(damage);
                break;

            case (int)TowerType.Third_tower:
                target.GetComponent<EnemyScript>().StartSlow(3, 1);
                target.GetComponent<EnemyScript>().TakeDamage(damage);
                break;

            case (int)TowerType.Fourth_tower:
                target.GetComponent<EnemyScript>().AOEDamage(2, damage);
                break;
        }
        Destroy(gameObject);
    }
}
