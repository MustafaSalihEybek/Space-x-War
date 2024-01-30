using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelItem : MonoBehaviour
{
    [SerializeField] public TMP_Text txtStage;
    [HideInInspector] public int stageIn;
    [HideInInspector] public bool isOpened = true;
    private Button btnLevel;

    private void Awake()
    {
        btnLevel = GetComponent<Button>();
        btnLevel.onClick.AddListener(LoadGameScene);
    }

    private void LoadGameScene()
    {
        if (isOpened)
        {
            SceneManager.LoadScene("Level " + stageIn);
            PlayerPrefs.SetInt("CurrentLevel", stageIn);
        }
    }
}
