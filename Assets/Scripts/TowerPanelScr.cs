using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerPanelScr : MonoBehaviour
{
    public TowerType selfType;
    public CellScript selfCell;
    public TowerScript towerS;
    public Image TowerLogo;
    public Text TowerName, TowerLvl, TowerDamage, TowerRange, TowerCooldown, TowerSellPrice, TowerUpPrice;
    public GameObject towerT;

    public void SetTowerData(TowerScript ts)
    {
        TowerLogo.sprite = ts.spr;
        TowerName.text = ts.Name;
        TowerDamage.text = ts.damage.ToString();
        TowerLvl.text = ts.lvl.ToString();
        TowerSellPrice.text = ((ts.price + ts.updatePrice / 2) / 2).ToString();
        TowerUpPrice.text = ts.updatePrice.ToString();
        TowerRange.text = ts.range.ToString();
        TowerCooldown.text = ts.cooldown.ToString();
    }

    public void UpgradeTower()
    {
        towerT = selfCell.ChoseTower();
        towerS = towerT.GetComponent<TowerScript>();
        if (GameManagerScr.Instance.gameMoney >= towerS.updatePrice)
        {
            GameManagerScr.Instance.gameMoney -= towerS.updatePrice;
            towerS.damage += 10;
            towerS.lvl += 1;
            towerS.range += (float)0.5;
            towerS.updatePrice += 20;
            ClosePanel();
        }
    }

    public void SellTower()
    {
        selfCell.DestroyTower();
        ClosePanel();
    }

    public void ClosePanel()
    {
        Destroy(gameObject);
    }
}
