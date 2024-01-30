using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private Button btnClose;
    [SerializeField] private TMP_Text[] shipLevels;
    [SerializeField] private Slider[] ship1Sliders;
    [SerializeField] private Slider[] ship2Sliders;
    [SerializeField] private Slider[] ship3Sliders;
    [SerializeField] private Slider[] ship4Sliders;
    [SerializeField] private Slider[] ship5Sliders;
    [SerializeField] private Button[] buttonUses;
    [SerializeField] private Button[] buttonBuys;
    [SerializeField] private TMP_Text[] txtUses;
    [SerializeField] private TMP_Text[] shipsCoinAmount;
    [SerializeField] private CurrentShip currentShip;
    [SerializeField] private Mainmanager mainmanager;

    private List<Slider[]> shipsSliders; // Health - Defense - Attack
    private ShipSettings shipsSettings;
    private ShipSetting shipSetting;
    private Dictionary<string, ShipSetting[]> shipsProperties;
    private int shipLevel;
    private Slider[] shipSliders;
    private string currentShipType;
    private int currentShipIn;
    private int neededCoinAmount;
    private readonly string[] shipTypeTags = new string[]
    {
        "ShipType1", "ShipType2", "ShipType3",
        "ShipType4", "ShipType5"
    };
    private readonly string[] shipBuyedTags = new string[]
    {
        "IsBuyedShipType1", "IsBuyedShipType2", "IsBuyedShipType3",
        "IsBuyedShipType4", "IsBuyedShipType5"
    };
    private readonly string[] shipLevelTags = new string[]
    {
        "ShipType1Level", "ShipType2Level", "ShipType3Level",
        "ShipType4Level", "ShipType5Level"
    };

    private void Start()
    {
        btnClose.onClick.AddListener(CloseThisPanel);

        shipsSettings = new ShipSettings();
        shipsProperties = shipsSettings.GetShipsProperties();
        currentShipType = PlayerPrefs.GetString("CurrentShipType");

        shipsSliders = new List<Slider[]>()
        {
            ship1Sliders, ship2Sliders, ship3Sliders,
            ship4Sliders, ship5Sliders
        };

        for (int i = 0; i < shipsSliders.Count; i++)
        {
            if (PlayerPrefs.GetString(shipBuyedTags[i]) == "yes")
            {
                shipLevel = PlayerPrefs.GetInt(shipLevelTags[i]);
                SetShipItemProperties(shipLevel, i);
                if (currentShipType == shipTypeTags[i]) txtUses[i].text = "USED";
                else txtUses[i].text = "USE";
            }
            else
            {
                shipLevel = 1;
                SetShipItemProperties(shipLevel, i);

                if (i > 0)
                {
                    buttonUses[i].gameObject.SetActive(true);
                    shipsCoinAmount[(i - 1)].transform.parent.gameObject.SetActive(true);
                    shipsCoinAmount[(i - 1)].text = shipsProperties[shipTypeTags[i]][0].GetShipCoinAmount().ToString();
                }
            }
        }
    }

    private void SetShipItemProperties(int shipLevel, int i)
    {
        shipLevels[i].text = "Level " + shipLevel;
        shipSliders = shipsSliders[i];
        shipSetting = shipsProperties[shipTypeTags[i]][(shipLevel - 1)];

        shipSliders[0].maxValue = shipsProperties[shipTypeTags[i]][(shipsProperties[shipTypeTags[i]].Length - 1)].GetShipHealth();
        shipSliders[0].value = shipSetting.GetShipHealth();

        shipSliders[1].maxValue = shipsProperties[shipTypeTags[i]][(shipsProperties[shipTypeTags[i]].Length - 1)].GetShipDefense();
        shipSliders[1].value = shipSetting.GetShipDefense();

        shipSliders[2].maxValue = shipsProperties[shipTypeTags[i]][(shipsProperties[shipTypeTags[i]].Length - 1)].GetShipAttack();
        shipSliders[2].value = shipSetting.GetShipAttack();
    }

    public void ChangeUsedShip(int i)
    {
        if (txtUses[i].text == "USE")
        {
            currentShip.SetCurrentShip(shipTypeTags[i]);
            txtUses[i].text = "USED";
            currentShipIn = GetCurrentShipIn(currentShipType);
            txtUses[currentShipIn].text = "USE";
            PlayerPrefs.SetString("CurrentShipType", shipTypeTags[i]);
            currentShipType = shipTypeTags[i];
        }
    }

    private int GetCurrentShipIn(string currentShipType)
    {
        for (int i = 0; i < shipTypeTags.Length; i++)
            if (shipTypeTags[i] == currentShipType) return i;

        return 0;
    }

    public void BuyShip(int i)
    {
        if (buttonBuys[i].IsActive())
        {
            neededCoinAmount = int.Parse(shipsCoinAmount[i].text);

            if (mainmanager.GetUserEarnedCoinAmount() >= neededCoinAmount)
            {
                mainmanager.DecreaseUserEarnedCoinAmount(neededCoinAmount);
                txtUses[(i + 1)].text = "USE";

                PlayerPrefs.SetString(shipBuyedTags[(i + 1)], "yes");
                PlayerPrefs.SetInt(shipLevelTags[(i + 1)], 1);

                buttonBuys[i].gameObject.SetActive(false);
                buttonUses[(i + 1)].gameObject.SetActive(true);
            }
        }
    }

    private void CloseThisPanel() { gameObject.SetActive(false); }
}
