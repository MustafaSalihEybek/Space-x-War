using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class YouWinPanel : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnReplay;
    [SerializeField] private Button btnNext;

    [Header("TxtCoin")]
    [SerializeField] private TMP_Text txtCoin;

    [Header("Mainmanager")]
    [SerializeField] private Gamemanager gamemanager;

    private void Awake()
    {
        btnHome.onClick.AddListener(BackTheMainScene);
        btnReplay.onClick.AddListener(PlayThisScene);
        btnNext.onClick.AddListener(NextTheScene);
    }

    public void SetEarnedCoin(int coin) 
    { 
        txtCoin.text = coin.ToString();
        if ((gamemanager.GetCurrentLevel() + 1) < 30) PlayerPrefs.SetInt("MaxOpenedLevel", (gamemanager.GetCurrentLevel() + 1));
    }

    private void BackTheMainScene() { LoadScene("MainScene"); }

    private void PlayThisScene() { LoadScene("Level " + (gamemanager.GetCurrentLevel())); }

    private void NextTheScene() 
    {
        PlayerPrefs.SetInt("CurrentLevel", (gamemanager.GetCurrentLevel() + 1));
        LoadScene("Level " + (gamemanager.GetCurrentLevel() + 1)); 
    }

    private void LoadScene(string sceneName) { SceneManager.LoadScene(sceneName); }
}
