using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StagesPanel : MonoBehaviour
{
    [SerializeField] private Button btnBack;
    [SerializeField] private GameObject[] twoLevelsList;

    private GameObject twoLevels;
    private GameObject levelItem;
    private LevelItem levelItemScript;
    private TMP_Text txtLevel;
    private Image imgLevel;
    private int maxOpenedLevel;
    private int c = 1;

    private void Awake()
    {
        btnBack.onClick.AddListener(CloseThePanel);
        maxOpenedLevel = PlayerPrefs.GetInt("MaxOpenedLevel", 1);

        for (int i = 0; i < twoLevelsList.Length; i++)
        {
            twoLevels = twoLevelsList[i];

            for (int j = 0; j < twoLevels.transform.childCount; j++)
            {
                levelItem = twoLevels.transform.GetChild(j).gameObject;

                levelItemScript = levelItem.GetComponent<LevelItem>();
                levelItemScript.stageIn = c;
                levelItemScript.isOpened = true;

                txtLevel = levelItem.transform.GetChild(0).GetComponent<TMP_Text>();
                txtLevel.text = c.ToString();

                if (!(c <= maxOpenedLevel))
                {
                    imgLevel = levelItem.GetComponent<Image>();
                    imgLevel.color = new Color32(255, 255, 255, 150);
                    txtLevel.color = new Color32(255, 255, 255, 150);
                    levelItemScript.isOpened = false;
                }

                c++;
            }
        }
    }

    private void CloseThePanel() { gameObject.SetActive(false); }
}
