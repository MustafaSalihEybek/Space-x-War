using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class YouLostPanel : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnReplay;
    [SerializeField] private Button btnBack;

    [Header("TxtCoin")]
    [SerializeField] private TMP_Text txtCoin;

    private void Awake()
    {
        btnHome.onClick.AddListener(BackTheMainScene);
        btnReplay.onClick.AddListener(PlayThisScene);
        btnBack.onClick.AddListener(BackTheMainScene);
    }

    public void SetEarnedCoin(int coin) { txtCoin.text = coin.ToString(); }

    private void BackTheMainScene() { LoadScene("MainScene"); }

    private void PlayThisScene() { LoadScene("Level1"); }

    private void LoadScene(string sceneName) { SceneManager.LoadScene(sceneName); }
}
