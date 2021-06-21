using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScr : MonoBehaviour
{
    [SerializeField]public static GameManagerScr Instance;
    [SerializeField] public Text moneyTxt;
    [SerializeField] public int gameMoney;
    public Text timeToSpawnTxt;
    public GameObject menu;
    public GameObject losePanel;
    public bool canSpawn = true;
    public Button skipTimeButton;
    public int yourHP;
    public Text hpText;

    private int _startMoney;
    private int _timeToSpawn;


    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _startMoney = gameMoney;
        Time.timeScale = 1;
    }

    void Update()
    {
        moneyTxt.text = gameMoney.ToString();
        hpText.text = yourHP.ToString();
        _timeToSpawn = (int)FindObjectOfType<EnemySpawnerScript>().timeToSpawn + 1;
        timeToSpawnTxt.text = _timeToSpawn.ToString();
        if (FindObjectOfType<EnemySpawnerScript>().timeToSpawn < 15 || FindObjectOfType<EnemySpawnerScript>().spawnCount == 0)
            skipTimeButton.interactable = true;
        else
            skipTimeButton.interactable = false;

        if (yourHP == 0)
        {
            Lose();
        }
    }

    public void PlayBtn()
    {
        menu.SetActive(false);
        canSpawn = true;
        Time.timeScale = 1;
    }

    public void Lose()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void SettingBtn()
    {

    }

    public void RestartButton()
    {
        foreach (CellScript cs in FindObjectsOfType<CellScript>())
            Destroy(cs.gameObject);
        foreach (TowerScript ts in FindObjectsOfType<TowerScript>())
            Destroy(ts.gameObject);
        foreach (EnemyScript es in FindObjectsOfType<EnemyScript>())
            Destroy(es.gameObject);

        gameMoney = _startMoney;

        FindObjectOfType<LvlManager>().CreateLevel();
        FindObjectOfType<EnemySpawnerScript>().spawnCount = 0;
        FindObjectOfType<EnemySpawnerScript>().timeToSpawn = FindObjectOfType<EnemySpawnerScript>().spawnTime;
        menu.SetActive(false);
        canSpawn = true;
        Time.timeScale = 1;
    }

    public void QuitBtn()
    {
        Application.Quit();
    }

    public void ToMenu()
    {
        canSpawn = false;
        menu.SetActive(true);

        if (FindObjectsOfType<ShopScript>().Length > 0)
            Destroy(FindObjectOfType<ShopScript>().gameObject);
        if (FindObjectsOfType<TowerPanelScr>().Length > 0)
            Destroy(FindObjectOfType<TowerPanelScr>().gameObject);

        Time.timeScale = 0;
    }

    public void SkipTime()
    {
        FindObjectOfType<EnemySpawnerScript>().timeToSpawn = 0;
    }
}
