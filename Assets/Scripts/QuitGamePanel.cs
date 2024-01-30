using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitGamePanel : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Gamemanager gamemanager = null;

    [Header("Buttons")]
    [SerializeField] private Button btnNo;
    [SerializeField] private Button btnYes;

    private void Awake()
    {
        btnNo.onClick.AddListener(CloseThisPanel);
        btnYes.onClick.AddListener(BackTheMainScene);
    }

    private void CloseThisPanel()
    {
        if (gamemanager != null) gamemanager.IsQuitGamePanelShowed = false;
        gameObject.SetActive(false);
    }

    private void BackTheMainScene() 
    {
        if (gamemanager != null) SceneManager.LoadScene("MainScene");
        else Application.Quit();
    }
}
