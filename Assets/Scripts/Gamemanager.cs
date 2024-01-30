using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TMP_Text txtCoin;
    [SerializeField] private TMP_Text txtWave;
    [SerializeField] private GameObject[] waveList;
    [SerializeField] private Image imgNextWave;
    [SerializeField] private TMP_Text txtNextWave;
    [SerializeField] private Player player;
    [SerializeField] private Button btnExit;

    [Header("Panels")]
    [SerializeField] private GameObject youWinPanel;
    [SerializeField] private GameObject youLostPanel;
    [SerializeField] private GameObject quitGamePanel;

    private int wIn = 0;
    private byte nIn = 1;
    private int userEarnedCoin = 0;
    private int userEarndedCoinAmount;
    private bool isGameOver = false;
    private bool isNextWaveShowed = false;
    private bool isOrderNextWave = false;
    private bool isQuitGamePanelShowed = false;
    private int currentLevel;
    private AudioSource gameMusic;
    private string musicIsOn;

    private void Start()
    {
        userEarndedCoinAmount = PlayerPrefs.GetInt("UserEarnedCoinAmount", 0);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        musicIsOn = PlayerPrefs.GetString("MusicIsOn", "true");
        gameMusic = GetComponent<AudioSource>();
        if (musicIsOn == "false")
        {
            gameMusic.playOnAwake = false;
            gameMusic.Stop();
        }
        SetWave((wIn + 1));
        SetUserEarnedCoin(0);
        btnExit.onClick.AddListener(ShowQuitGamePanel);
    }

    private void Update()
    {
        if (IsEnemiesDeadInWave(waveList[wIn]) && wIn < (waveList.Length - 1))
        {
            isOrderNextWave = false;
            CalculateEarnedCoin(waveList[wIn]);
            SetHideInWaveEnemies(waveList[wIn], false);
            wIn++;
            SetWave((wIn + 1));
            if (!isOrderNextWave) InvokeRepeating("ShowNextWave", 0f, 0.01f);
        }
        else if (IsEnemiesDeadInWave(waveList[wIn]) && wIn == (waveList.Length - 1))
        {
            if (!isGameOver)
            {
                isGameOver = true;
                CalculateEarnedCoin(waveList[wIn]);
                SetHideInWaveEnemies(waveList[wIn], false);
                ShowYouWinPanel();
            }
        }

        if (player.GetPlayerHealth() == 0 && !isGameOver)
        {
            isGameOver = true;
            ShowYouLostPanel();
        }
    }

    public int UserEarnedCoin
    {
        get { return userEarnedCoin; }
        set { userEarnedCoin = value; }
    }

    public bool IsQuitGamePanelShowed
    {
        get { return isQuitGamePanelShowed; }
        set { isQuitGamePanelShowed = value; }
    }

    public void SetUserEarnedCoin(int coin)
    {
        txtCoin.text = coin.ToString();
    }

    public bool GetIsGameOver() => isGameOver;

    public int GetCurrentLevel() => currentLevel;

    public void SavedEarnedCoinAmount(int coin)
    {
        PlayerPrefs.SetInt("UserEarnedCoinAmount", (userEarndedCoinAmount + coin));
    }

    private void SetWave(int wIn)
    {
        txtWave.text = "WAVE " + wIn + "/3";
    }

    private bool IsEnemiesDeadInWave(GameObject wave)
    {
        for (int i = 0; i < wave.transform.childCount; i++)
        {
            if (wave.transform.GetChild(i).GetComponent<Enemy>().GetEnemyHealth() != 0)
                return false;
        }

        return true;
    }

    private void SetHideInWaveEnemies(GameObject wave, bool state)
    {
        for (int i = 0; i < wave.transform.childCount; i++)
            wave.transform.GetChild(i).gameObject.SetActive(state);
    }

    private void CalculateEarnedCoin(GameObject wave)
    {
        for (int i = 0; i < wave.transform.childCount; i++)
            wave.transform.GetChild(i).GetComponent<Enemy>().EarnCoin();
    }

    private void ShowNextWave()
    {
        if (!isOrderNextWave)
        {
            if (!isNextWaveShowed)
            {
                nIn++;

                if (nIn == 215)
                    isNextWaveShowed = true;
            }

            if (isNextWaveShowed)
            {
                nIn--;

                if (nIn == 0)
                    isNextWaveShowed = false;
            }

            imgNextWave.color = new Color32(34, 34, 34, nIn);
            txtNextWave.color = new Color32(255, 255, 255, nIn);

            if (nIn == 0)
            {
                isOrderNextWave = true;
                CancelInvoke("ShowNextWave");
                SetHideInWaveEnemies(waveList[wIn], true);
            }
        }
    }

    private void ShowYouWinPanel()
    {
        youWinPanel.GetComponent<YouWinPanel>().SetEarnedCoin(userEarnedCoin);
        youWinPanel.SetActive(true);
    }

    private void ShowYouLostPanel()
    {
        youLostPanel.GetComponent<YouLostPanel>().SetEarnedCoin(userEarnedCoin);
        youLostPanel.SetActive(true);
    }

    private void ShowQuitGamePanel()
    {
        isQuitGamePanelShowed = true;
        quitGamePanel.SetActive(true);
    }
}
