using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentShip : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Sprite[] shipSprites;
    [SerializeField] private Image imgShip;
    [SerializeField] private TMP_Text txtShipType;
    [SerializeField] private TMP_Text txtShipLevel;
    [SerializeField] private Button improveButton;
    [SerializeField] private ImprovePanel improvePanel;

    [Header("Sliders")]
    [SerializeField] private Slider shipHealthSlider;
    [SerializeField] private Slider shipDefenseSlider;
    [SerializeField] private Slider shipAttackSlider;

    private ShipSettings shipSettings;
    private ShipSetting shipSetting;
    private Dictionary<string, ShipSetting[]> shipsProperties;
    private int shipLevel;

    private void Awake()
    {
        improveButton.onClick.AddListener(ShowImprovePanel);
    }

    public void SetCurrentShip(string shipType)
    {
        shipSettings = new ShipSettings();
        shipsProperties = shipSettings.GetShipsProperties();

        shipLevel = GetShipLevel(shipType);
        imgShip.sprite = GetShipSprite(shipType);
        txtShipType.text = GetShipTypeForText(shipType);

        shipSetting = shipsProperties[shipType][(shipLevel - 1)];
        txtShipLevel.text = "Level " + shipLevel;

        shipHealthSlider.maxValue = shipsProperties[shipType][(shipsProperties[shipType].Length - 1)].GetShipHealth();
        shipHealthSlider.value = shipSetting.GetShipHealth();

        shipDefenseSlider.maxValue = shipsProperties[shipType][(shipsProperties[shipType].Length - 1)].GetShipDefense();
        shipDefenseSlider.value = shipSetting.GetShipDefense();

        shipAttackSlider.maxValue = shipsProperties[shipType][(shipsProperties[shipType].Length - 1)].GetShipAttack();
        shipAttackSlider.value = shipSetting.GetShipAttack();

        improvePanel.SetCurrentImproveShip(shipType);
    }

    private int GetShipLevel(string shipType)
    {
        int shipLevel = 1;

        switch (shipType)
        {
            case "ShipType1":
                shipLevel = PlayerPrefs.GetInt("ShipType1Level", 1);
                break;

            case "ShipType2":
                shipLevel = PlayerPrefs.GetInt("ShipType2Level", 1);
                break;

            case "ShipType3":
                shipLevel = PlayerPrefs.GetInt("ShipType3Level", 1);
                break;

            case "ShipType4":
                shipLevel = PlayerPrefs.GetInt("ShipType4Level", 1);
                break;

            case "ShipType5":
                shipLevel = PlayerPrefs.GetInt("ShipType5Level", 1);
                break;
        }

        return shipLevel;
    }

    private Sprite GetShipSprite(string shipType)
    {
        Sprite shipSprite = shipSprites[0];

        switch (shipType)
        {
            case "ShipType1":
                shipSprite = shipSprites[0];
                break;

            case "ShipType2":
                shipSprite = shipSprites[1];
                break;

            case "ShipType3":
                shipSprite = shipSprites[2];
                break;

            case "ShipType4":
                shipSprite = shipSprites[3];
                break;

            case "ShipType5":
                shipSprite = shipSprites[4];
                break;
        }

        return shipSprite;
    }

    private string GetShipTypeForText(string shipType)
    {
        string shipTypeText = "1.SHIP";

        switch (shipType)
        {
            case "ShipType1":
                shipTypeText = "1.SHIP";
                break;

            case "ShipType2":
                shipTypeText = "2.SHIP";
                break;

            case "ShipType3":
                shipTypeText = "3.SHIP";
                break;

            case "ShipType4":
                shipTypeText = "4.SHIP";
                break;

            case "ShipType5":
                shipTypeText = "5.SHIP";
                break;
        }

        return shipTypeText;
    }

    private void ShowImprovePanel() { improvePanel.gameObject.SetActive(true); }
}
