using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle soundEffectToggle;
    [SerializeField] private Button btnClose;
    [SerializeField] private Mainmanager mainmanager;

    private void Awake()
    {
        musicToggle.isOn = PlayerPrefs.GetString("MusicIsOn", "true") == "true" ? true : false;
        soundEffectToggle.isOn = PlayerPrefs.GetString("SoundEffectIsOn", "true") == "true" ? true : false;
        btnClose.onClick.AddListener(CloseThisPanel);
    }

    private void CloseThisPanel()
    {
        PlayerPrefs.SetString("MusicIsOn", musicToggle.isOn ? "true" : "false");
        PlayerPrefs.SetString("SoundEffectIsOn", soundEffectToggle.isOn ? "true" : "false");
        mainmanager.SetMusic();
        gameObject.SetActive(false);
    }
}
