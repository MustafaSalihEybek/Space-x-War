using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float upSpeed;
    [SerializeField] GameObject playerBullet;
    [SerializeField] Transform bulletPoint;
    [SerializeField] Transform playerBullets;
    [SerializeField] HealthBar healthBar;
    [SerializeField] Gamemanager gamemanager;
    [SerializeField] Sprite[] shipSprites;

    private int playerHealth = 100;
    private int playerDefense = 5;
    private int playerAttack = 20;
    private int enemyDamage;
    private float vPos;
    private bool isFired = false;
    private bool isHitWallTop = false;
    private bool isHitWallBottom = false;
    private GameObject createdBullet;
    private Bullet createdBulletScript;
    private string currentShipType;
    private Image imgShip;
    private int shipLevel;
    private ShipSettings shipSettings;
    private ShipSetting shipSetting;
    private Dictionary<string, ShipSetting[]> shipsProperties;
    private bool isUp = false;
    private bool isDown = false;
    private float vPosSpeed = 0f;

    private void Start()
    {
        imgShip = GetComponent<Image>();
        upSpeed = 3.5f;

        currentShipType = PlayerPrefs.GetString("CurrentShipType");
        imgShip.sprite = GetShipSprite(currentShipType);
        shipLevel = GetShipLevel(currentShipType);

        shipSettings = new ShipSettings();
        shipsProperties = shipSettings.GetShipsProperties();
        shipSetting = shipsProperties[currentShipType][(shipLevel - 1)];

        playerHealth = shipSetting.GetShipHealth();
        playerDefense = shipSetting.GetShipDefense();
        playerAttack = shipSetting.GetShipAttack();

        healthBar.SetMaxHealth(playerHealth);
    }

    private void Update()
    {
        if (!gamemanager.GetIsGameOver() && !gamemanager.IsQuitGamePanelShowed)
        {
            if (Settings.isPlayPc)
            {
                MoveThePlayerForPc();
                FireThePlayerForPc();
            }
            else
            {
                if (!isHitWallTop && isUp)
                {
                    if (vPosSpeed < 1f)
                        vPosSpeed += 0.005f;

                    transform.position += Vector3.up * upSpeed * vPosSpeed;
                }

                if (!isHitWallBottom && isDown)
                {
                    if (vPosSpeed > -1f)
                        vPosSpeed -= 0.005f;

                    transform.position += Vector3.up * upSpeed * vPosSpeed;
                }
            }
        }
    }

    private void MoveThePlayerForPc()
    {
        vPos = Input.GetAxis("Vertical");
        if (vPos > 0 && !isHitWallTop) transform.position += Vector3.up * upSpeed * vPos;
        if (vPos < 0 && !isHitWallBottom) transform.position += Vector3.up * upSpeed * vPos;
    }

    private void FireThePlayerForPc()
    {
        if (Input.GetMouseButtonDown(0)) FireThePlayer();
    }

    public void FireThePlayer()
    {
        if (!isFired)
        {
            isFired = true;
            createdBullet = Instantiate(playerBullet, bulletPoint.position, Quaternion.identity, playerBullets);
            createdBulletScript = createdBullet.GetComponent<Bullet>();
            createdBulletScript.bulletSpeed = 7f;
            createdBulletScript.isBulletPlayer = true;
            createdBulletScript.bulletAttack = playerAttack;
            Invoke("ReadyFire", 0.5f);
        }
    }

    public void MoveUpForAndroid()
    {
        if (isUp) vPosSpeed = 0f;
        isUp = !isUp;
    }

    public void MoveDownForAndroid()
    {
        if (isDown) vPosSpeed = 0f;
        isDown = !isDown;
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

    private void ReadyFire() { isFired = false; }

    public int GetPlayerHealth() => playerHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            createdBulletScript = collision.GetComponent<Bullet>();
            enemyDamage = createdBulletScript.bulletAttack;
            if ((playerHealth - (enemyDamage - playerDefense)) > 0) playerHealth -= (enemyDamage - playerDefense);
            else playerHealth -= (playerHealth);
            healthBar.SetHealth(playerHealth);
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "WallTop")
            isHitWallTop = true;
        else if (collision.tag == "WallBottom")
            isHitWallBottom = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "WallTop")
            isHitWallTop = false;
        else if (collision.tag == "WallBottom")
            isHitWallBottom = false;
    }
}
