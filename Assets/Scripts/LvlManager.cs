using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LvlManager : MonoBehaviour
{
    public int fieldWidth, fieldHeight;
    public GameObject cellPref;

    public Transform cellParent;

    public Sprite[] tileSprite = new Sprite[3];

    public List<GameObject> wayPoints = new List<GameObject>();

    string[] path = { "000100000000000000",
                      "000100000000111111",
                      "000120000000120000",
                      "000100020020100000",
                      "002120000111100000",
                      "000120000120000000",
                      "002111111100000000",
                      "000002000000000000",
                      "000000000000000000"};

    GameObject[,] allCells = new GameObject[10, 20];

    int currWayX, currWayY;
    GameObject firstCell;

    private void Start()
    {
        CreateLevel();
    }

    public void CreateLevel()
    {
        wayPoints.Clear();
        firstCell = null;

        Vector3 worldVec = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));

        for (int i = 0; i < fieldHeight; i++)
            for (int j = 0; j < fieldWidth; j++)
            {
                int sprIndex = int.Parse(path[i].ToCharArray()[j].ToString());            //(LoadLevelText(1)[i].ToCharArray()[j].ToString());  
                Sprite spr = tileSprite[sprIndex];

                bool isGround = spr == tileSprite[1] ? true : false;
                bool isTower = spr == tileSprite[2] ? true : false;

                CreateCell(isGround, isTower, spr, j, i, worldVec);
            }
        LoadWayPoints();
    }

    void CreateCell(bool isGround, bool isTower, Sprite spr, int x, int y, Vector3 wV)
    {
        GameObject tmpCell = Instantiate(cellPref);
        tmpCell.transform.SetParent(cellParent, false);

        tmpCell.GetComponent<SpriteRenderer>().sprite = spr;

        float sprSizeX = tmpCell.GetComponent<SpriteRenderer>().bounds.size.x;
        float sprSizeY = tmpCell.GetComponent<SpriteRenderer>().bounds.size.y;

        tmpCell.transform.position = new Vector3(wV.x + (sprSizeX * x), wV.y + (sprSizeY * -y));

        if (isTower)
        {
            tmpCell.GetComponent<CellScript>().isTower = true;
        }

        if(isGround)
        {
            tmpCell.GetComponent<CellScript>().isGround = true;

            if(firstCell == null)
            {
                firstCell = tmpCell;
                currWayX = x;
                currWayY = y;
            }
        }

        allCells[y, x] = tmpCell;
    }

    string[] LoadLevelText(int i)
    {
        TextAsset tmpText = Resources.Load<TextAsset>("Level" + i + "Ground");

        string tmpStr = tmpText.text.Replace(Environment.NewLine, string.Empty);

        return tmpStr.Split('!');
    }

    void LoadWayPoints()
    {
        GameObject currWayGO;
        wayPoints.Add(firstCell);

        while(true)
        {
            currWayGO = null;

            if (currWayX > 0 && allCells[currWayY, currWayX - 1].GetComponent<CellScript>().isGround &&
                !wayPoints.Exists(x => x == allCells[currWayY, currWayX - 1]))
            {
                currWayGO = allCells[currWayY, currWayX - 1];
                currWayX--;
            }
            else if (currWayX < 16 && allCells[currWayY, currWayX + 1].GetComponent<CellScript>().isGround &&
                !wayPoints.Exists(x => x == allCells[currWayY, currWayX + 1]))
            {
                currWayGO = allCells[currWayY, currWayX + 1];
                currWayX++;
            }
            else if (currWayY > 0 && allCells[currWayY - 1, currWayX].GetComponent<CellScript>().isGround &&
                !wayPoints.Exists(x => x == allCells[currWayY - 1, currWayX]))
            {
                currWayGO = allCells[currWayY - 1, currWayX];
                currWayY--;
            }
            else if (currWayX < (fieldHeight - 1) && allCells[currWayY + 1, currWayX].GetComponent<CellScript>().isGround &&
                !wayPoints.Exists(x => x == allCells[currWayY + 1, currWayX]))
            {
                currWayGO = allCells[currWayY + 1, currWayX];
                currWayY++;
            }
            else
                break;

            wayPoints.Add(currWayGO);
        }
    }
}
