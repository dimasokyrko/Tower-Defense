using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItemScript : MonoBehaviour, IPointerClickHandler
{
    Tower selfTower;
    CellScript selfCell;
    public Image TowerLogo;
    public Text TowerName, TowerPrice;

    public void SetStartData(Tower tower, CellScript cell)
    {
        selfTower = tower;
        TowerLogo.sprite = tower.spr;
        TowerName.text = tower.Name;
        TowerPrice.text = tower.price.ToString();
        selfCell = cell;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(GameManagerScr.Instance.gameMoney >= selfTower.price)
        {
            selfCell.BuildTower(selfTower);
            GameManagerScr.Instance.gameMoney -= selfTower.price;
        }
    }
}
