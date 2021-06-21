using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Tower
{
    public string Name;
    public int type, price, damage, updatePrice, lvl;
    public float range, cooldown;
    public Sprite spr;

    public Tower(string Name, int price, int damage, int type, float range, float cd, string path, int lvl, int updatePrice)
    {
        this.Name = Name;
        this.price = price;
        this.type = type;
        this.lvl = lvl;
        this.updatePrice = updatePrice;
        this.range = range;
        this.damage = damage;
        cooldown = cd;
        spr = Resources.Load<Sprite>(path);
    }
}

public class TowerProjectile
{
    public float speed;
    public int damage;
    public Sprite Spr;

    public TowerProjectile(float speed, int dmg, string path)
    {
        this.speed = speed;
        damage = dmg;
        Spr = Resources.Load<Sprite>(path);
    }
}

public class Enemy
{
    public float Health, Speed, startSpeed;
    public Sprite spr;

    public Enemy(float health, float speed, string path)
    {
        Health = health;
        startSpeed = Speed = speed;
        spr = Resources.Load<Sprite>(path);
    }

    public Enemy(Enemy other)
    {
        Health = other.Health;
        startSpeed = Speed = other.startSpeed;
        spr = other.spr;
    }
}

/*public class EnemySpawn
{
    public int wave;
    public int firstTypeCount;
    public int secondTypeCount;
    public EnemySpawn(int Wave, int FirstTypeCount, int SecondTypeCount)
    {
        wave = Wave;
        firstTypeCount = FirstTypeCount;
        secondTypeCount = SecondTypeCount;
        
    }
}*/

public enum TowerType
{
    First_tower,
    Second_tower,
    Third_tower,
    Fourth_tower
}

public class GameControllerScript : MonoBehaviour
{
    public List<Tower> AllTowers = new List<Tower>();
    public List<TowerProjectile> AllProjectiles = new List<TowerProjectile>();
    public List<Enemy> AllEnemies = new List<Enemy>();
    //public List<EnemySpawn> AllSpawns = new List<EnemySpawn>();
    public int fType = 5, sType = 0;

    private void Awake()
    {
        //В скобках (Name, price, damage, type, range, cooldown, sprite, lvl, updateprice)
        AllTowers.Add(new Tower("CommonTower", 30, 15, 0, 4, 1, "TowerSprites/FirstTower",1 , 30));
        AllTowers.Add(new Tower("SniperTower", 50, 40, 1, 7, 2, "TowerSprites/SecondTower", 1, 50));
        AllTowers.Add(new Tower("SlowTower", 40, 10, 2, 3, 2, "TowerSprites/ThirdTower", 1, 40));
        AllTowers.Add(new Tower("AOETower", 80, 30, 3, 4, 2, "TowerSprites/FourthTower", 1, 80));

        //В скобках (speed, damage, sprite)
        AllProjectiles.Add(new TowerProjectile(6, 10, "ProjectileSprites/FirstProjectile"));
        AllProjectiles.Add(new TowerProjectile(10, 30, "ProjectileSprites/SecondProjectile"));
        AllProjectiles.Add(new TowerProjectile(10, 30, "ProjectileSprites/SecondProjectile"));
        AllProjectiles.Add(new TowerProjectile(10, 30, "ProjectileSprites/SecondProjectile"));

        //В скобках (health, speed, sprite)
        AllEnemies.Add(new Enemy(20, 3, "EnemySprites/Enemy1"));
        AllEnemies.Add(new Enemy(60, 2, "EnemySprites/Enemy2"));

        //В скобках (wave, firstTypeCount, secondTypeCount)
        /*for(int i = 0; i < 10; i++) 
        {
            if (i % 5 == 0)
                fType += 3;
            else if (i % 2 == 0)
                fType += 2;
            else
            {
                fType -= 3;
                sType += 1;
            }
            AllSpawns.Add(new EnemySpawn(i, fType, sType));
        }*/
    }
}
