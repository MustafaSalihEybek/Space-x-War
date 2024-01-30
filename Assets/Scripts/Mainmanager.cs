using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mainmanager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnOptions;
    [SerializeField] private Button btnShop;
    [SerializeField] private Button btnExit;

    [Header("TxtCoin")]
    [SerializeField] private TMP_Text txtCoin;

    [Header("Panels")]
    [SerializeField] private GameObject quitGamePanel;
    [SerializeField] private CurrentShip currentShip;
    [SerializeField] private GameObject stagesPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject optionsPanel;

    private int userEarnedCoinAmount;
    private string currentShipType;
    private AudioSource mainMusic;
    private string musicIsOn;
    private float mainMusicVolume = 0.350f;

    private void Start()
    {
        mainMusic = GetComponent<AudioSource>();
        SetMusic();

        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.DeleteKey("MaxOpenedLevel");
        if (PlayerPrefs.GetString("IsFirstLoad", "false") == "false") SetFirstLoadData();

        userEarnedCoinAmount = PlayerPrefs.GetInt("UserEarnedCoinAmount", 0);
        txtCoin.text = userEarnedCoinAmount.ToString();

        currentShipType = PlayerPrefs.GetString("CurrentShipType");
        SetCurrentShipType(currentShipType);

        btnPlay.onClick.AddListener(ShowStagesPanel);
        btnOptions.onClick.AddListener(ShowOptionsPanel);
        btnShop.onClick.AddListener(ShowShopPanel);
        btnExit.onClick.AddListener(ShowQuitGamePanel);
    }

    private void ShowStagesPanel() { stagesPanel.SetActive(true); }

    private void ShowOptionsPanel() { optionsPanel.SetActive(true); }

    private void ShowShopPanel() { shopPanel.SetActive(true); }

    private void ShowQuitGamePanel() { quitGamePanel.SetActive(true); }

    private void SetFirstLoadData()
    {
        PlayerPrefs.SetString("IsBuyedShipType1", "yes");
        PlayerPrefs.SetString("IsBuyedShipType2", "no");
        PlayerPrefs.SetString("IsBuyedShipType3", "no");
        PlayerPrefs.SetString("IsBuyedShipType4", "no");
        PlayerPrefs.SetString("IsBuyedShipType5", "no");

        PlayerPrefs.SetInt("ShipType1Level", 1);
        PlayerPrefs.SetInt("ShipType2Level", 0);
        PlayerPrefs.SetInt("ShipType3Level", 0);
        PlayerPrefs.SetInt("ShipType4Level", 0);
        PlayerPrefs.SetInt("ShipType5Level", 0);

        PlayerPrefs.SetString("CurrentShipType", "ShipType1");
        PlayerPrefs.SetInt("UserEarnedCoinAmount", 0);

        PlayerPrefs.SetInt("CurrentLevel", 1);
        PlayerPrefs.SetInt("MaxOpenedLevel", 1);

        PlayerPrefs.SetString("IsFirstLoad", "true");
    }

    public void SetCurrentShipType(string shipType) { currentShip.SetCurrentShip(shipType); }

    public int GetUserEarnedCoinAmount() => userEarnedCoinAmount;

    public void DecreaseUserEarnedCoinAmount(int coin)
    {
        userEarnedCoinAmount -= coin;
        txtCoin.text = userEarnedCoinAmount.ToString();
        PlayerPrefs.SetInt("UserEarnedCoinAmount", userEarnedCoinAmount);
    }

    public void SetMusic()
    {
        musicIsOn = PlayerPrefs.GetString("MusicIsOn", "true");
        if (musicIsOn == "false")
        {
            mainMusic.playOnAwake = false;
            mainMusic.volume = 0f;
        }
        else
        {
            mainMusic.playOnAwake = true;
            mainMusic.volume = mainMusicVolume;
        }
    }
}
