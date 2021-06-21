using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    List<GameObject> wayPoints = new List<GameObject>();

    public Enemy selfEnemy;

    int wayIndex = 0;
    

    private void Start()
    {
        GetWayPoints();

        GetComponent<SpriteRenderer>().sprite = selfEnemy.spr;
    }

    void GetWayPoints()
    {
        wayPoints = GameObject.Find("LevelGroup").GetComponent<LvlManager>().wayPoints;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Transform currWayPoint = wayPoints[wayIndex].transform;
        Vector3 currWayPos = new Vector3(currWayPoint.position.x + currWayPoint.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                         currWayPoint.position.y - currWayPoint.GetComponent<SpriteRenderer>().bounds.size.y / 2);

        Vector3 dir = currWayPos - transform.position;

        transform.Translate(dir.normalized * Time.deltaTime * selfEnemy.Speed);

        if (Vector3.Distance(transform.position, currWayPos) < 0.1f)
        {
            if (wayIndex < wayPoints.Count - 1)
                wayIndex++;
            else
            {
                Destroy(gameObject);
                GameManagerScr.Instance.yourHP -= 1;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        selfEnemy.Health -= damage;
        CheckIsAlive();
    }

    void CheckIsAlive()
    {
        if (selfEnemy.Health <= 0)
        {
            GameManagerScr.Instance.gameMoney += 15;
            Destroy(gameObject);
        }
    }

    public void StartSlow(float duration, float slowValue)
    {
        StopCoroutine("GetSlow");
        selfEnemy.Speed = selfEnemy.startSpeed;
        StartCoroutine(GetSlow(duration, slowValue));
    }
    IEnumerator GetSlow(float duration, float slowValue)
    {
        selfEnemy.Speed -= slowValue;
        yield return new WaitForSeconds(duration);
        selfEnemy.Speed = selfEnemy.startSpeed;
    }

    public void AOEDamage(float range, float damage)
    {
        List<EnemyScript> enemies = new List<EnemyScript>();

        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Vector2.Distance(transform.position, go.transform.position) <= range)
                enemies.Add(go.GetComponent<EnemyScript>());
        }
        foreach (EnemyScript es in enemies)
            es.TakeDamage(damage);
    }
}
