using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public GameObject projectile;

    private float currCooldown;

    public Tower selfTower;
    public TowerType selfType;
    
    GameControllerScript gcs;

    public Sprite spr;
    public string Name;
    public int type, damage, updatePrice, lvl, price;
    public float range, cooldown;

    private void Start()
    {
        gcs = FindObjectOfType<GameControllerScript>();

        selfTower = gcs.AllTowers[(int)selfType];
        GetComponent<SpriteRenderer>().sprite = selfTower.spr;

        spr = selfTower.spr;
        damage = selfTower.damage;
        Name = selfTower.Name;
        range = selfTower.range;
        cooldown = selfTower.cooldown;
        updatePrice = selfTower.updatePrice;
        lvl = selfTower.lvl;
        price = selfTower.price;



        InvokeRepeating("SearchTarget", 0, 0.1f);
    }

    private void Update()
    {
        if (currCooldown > 0)
            currCooldown -= Time.deltaTime;
    }

    bool CanSoot()
    {
        if (currCooldown <= 0)
            return true;
        return false;
    }

    void SearchTarget()
    {
        if (CanSoot())
        {
            Transform nearestEnemy = null;
            float nearestEnemyDistance = Mathf.Infinity;

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                float currDistance = Vector2.Distance(transform.position, enemy.transform.position);

                if (currDistance < nearestEnemyDistance && currDistance <= range)
                {
                    nearestEnemy = enemy.transform;
                    nearestEnemyDistance = currDistance;
                }
            }

            if (nearestEnemy != null)
                Shoot(nearestEnemy);
        }
    }

    void Shoot(Transform enemy)
    {
        currCooldown = selfTower.cooldown;
        GameObject projec = Instantiate(projectile);
        projec.GetComponent<TowerProjectileScript>().selfTower = selfTower;
        projec.GetComponent<TowerProjectileScript>().damage = damage;
        projec.transform.position = transform.position;
        projec.GetComponent<TowerProjectileScript>().SetTarget(enemy);
    }
}
