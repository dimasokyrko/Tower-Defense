using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CellScript : MonoBehaviour
{
    public bool isGround, isTower;

    public Color BaseColor, CurrColor;

    public GameObject ShopPref, TowerPref;

    public GameObject TowerPanelPref;

    public GameControllerScript gcs;

    public GameObject SelfTower;

    GameObject choseTower;
    TowerScript ts;

    GameObject[] Towers;

    private void OnMouseDown()
    {
        if (isTower && !isGround && FindObjectsOfType<ShopScript>().Length == 0 && GameManagerScr.Instance.canSpawn
            && FindObjectsOfType<TowerPanelScr>().Length == 0)
        {
            if (!SelfTower)
            {
                GetComponent<SpriteRenderer>().color = BaseColor;
                GameObject shopObj = Instantiate(ShopPref);
                shopObj.transform.SetParent(GameObject.Find("Canvas").transform, false);
                shopObj.GetComponent<ShopScript>().selfCell = this;
            }
            else
            {
                Towers = GameObject.FindGameObjectsWithTag("Tower");
                choseTower = ChoseTower();
                ts = choseTower.GetComponent<TowerScript>();
                GameObject TowerObj = Instantiate(TowerPanelPref);
                TowerObj.GetComponent<TowerPanelScr>().SetTowerData(ts);
                TowerObj.transform.SetParent(GameObject.Find("Canvas").transform, false);
                TowerObj.GetComponent<TowerPanelScr>().selfCell = this;
            }
        }
    }

    public GameObject ChoseTower()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        GameObject chosenTower = null;
        foreach(GameObject t in Towers)
        {
            Vector3 diff = t.transform.position - position;
            float currDistance = diff.sqrMagnitude;
            if(currDistance < distance)
            {
                chosenTower = t;
                distance = currDistance;
            }
        }
        return chosenTower;
    }

    public void BuildTower(Tower tower)
    {
        GameObject tmpTower = Instantiate(TowerPref);
        tmpTower.transform.SetParent(transform, false);
        Vector2 towerpos = new Vector2(transform.position.x + (float)0.62,
                                       transform.position.y - (float)0.62);
        tmpTower.transform.position = towerpos;
        tmpTower.GetComponent<TowerScript>().selfType = (TowerType)tower.type;

        SelfTower = tmpTower;
        GetComponent<SpriteRenderer>().color = CurrColor;
        FindObjectOfType<ShopScript>().CloseShop();
    }

    public void DestroyTower()
    {
        GameManagerScr.Instance.gameMoney += (ts.GetComponent<TowerScript>().price + ts.GetComponent<TowerScript>().updatePrice / 2) / 2;
        Destroy(SelfTower);
    }
}