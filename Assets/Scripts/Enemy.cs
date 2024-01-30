using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private Transform enemyBullets;
    [SerializeField] private Sprite[] enemiesSprites;

    private RectTransform playerTransform;
    private Gamemanager gamemanager;
    private int enemyHealth = 100;
    private int enemyDefense = 5;
    private int enemyAttack = 10;
    private int enemyCoinAmount = 15;
    private int playerDamage;
    private RectTransform enemyTransform;
    private Vector2 playerPos;
    private bool takeDistanceOnce = false;
    private bool isDown = false;
    private bool isFire = false;
    private bool isFired = false;
    private GameObject createdBullet;
    private Bullet createdBulletScript;
    private int currentLevel;
    private Image imgShip;
    private Sprite shipSprite;
    private EnemiesSettings enemiesSettings;
    private Dictionary<int, ShipSetting> enemiesProperties;
    private ShipSetting enemySetting;
    private float speed;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<RectTransform>();
        gamemanager = GameObject.FindGameObjectWithTag("Gamemanager").GetComponent<Gamemanager>();
        enemyTransform = GetComponent<RectTransform>();

        moveSpeed = 2.85f;
        speed = moveSpeed;

        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        imgShip = GetComponent<Image>();

        if (currentLevel < 11) shipSprite = enemiesSprites[0];
        else if (currentLevel >= 11 && currentLevel < 21) shipSprite = enemiesSprites[1];
        else shipSprite = enemiesSprites[2];

        imgShip.sprite = shipSprite;

        enemiesSettings = new EnemiesSettings();
        enemiesProperties = enemiesSettings.GetEnemiesProperties();
        enemySetting = enemiesProperties[currentLevel];

        enemyHealth = enemySetting.GetShipHealth();
        enemyDefense = enemySetting.GetShipDefense();
        enemyAttack = enemySetting.GetShipAttack();
        enemyCoinAmount = enemySetting.GetShipCoinAmount();

        healthBar.SetMaxHealth(enemyHealth);
    }

    private void Update()
    {
        if (!gamemanager.GetIsGameOver() && !gamemanager.IsQuitGamePanelShowed)
        {
            MoveTheEnemey();
            FireTheEnemy();
        }
    }

    private void MoveTheEnemey()
    {
        if (!takeDistanceOnce)
        {
            playerPos = new Vector2(enemyTransform.anchoredPosition.x, playerTransform.anchoredPosition.y);

            if (playerPos.y < enemyTransform.anchoredPosition.y)
            {
                isDown = true;
                takeDistanceOnce = true;
                isFire = false;
            }
            else if (playerPos.y > enemyTransform.anchoredPosition.y)
            {
                isDown = false;
                takeDistanceOnce = true;
                isFire = false;
            }
            else
            {
                takeDistanceOnce = false;
                isFire = true;
            }
        }
        else
        {
            if (isDown)
            {
                if (enemyTransform.anchoredPosition.y > playerPos.y)
                {
                    if ((enemyTransform.anchoredPosition.y + (-1 * moveSpeed)) < playerPos.y)
                        speed = moveSpeed;
                    else 
                        speed = enemyTransform.anchoredPosition.y - playerPos.y;

                    enemyTransform.anchoredPosition += new Vector2(0, -speed);
                }
                else
                {
                    enemyTransform.anchoredPosition = playerPos;
                    takeDistanceOnce = false;
                }
            }
            else
            {
                if (enemyTransform.anchoredPosition.y < playerPos.y)
                {
                    if ((enemyTransform.anchoredPosition.y + (1 * moveSpeed)) > playerPos.y)
                        speed = moveSpeed;
                    else
                        speed = playerPos.y - enemyTransform.anchoredPosition.y;

                    enemyTransform.anchoredPosition += new Vector2(0, speed);
                }
                else
                {
                    enemyTransform.anchoredPosition = playerPos;
                    takeDistanceOnce = false;
                }
            }
        }
    }

    private void FireTheEnemy()
    {
        if (isFire)
        {
            if (!isFired)
            {
                isFired = true;
                createdBullet = Instantiate(enemyBullet, bulletPoint.position, Quaternion.identity, enemyBullets);
                createdBulletScript = createdBullet.GetComponent<Bullet>();
                createdBulletScript.bulletSpeed = 7f;
                createdBulletScript.isBulletPlayer = false;
                createdBulletScript.bulletAttack = enemyAttack;
                Invoke("ReadyFire", 0.5f);
            }
        }
    }

    private void ReadyFire() { isFired = false; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        createdBulletScript = collision.GetComponent<Bullet>();
        playerDamage = createdBulletScript.bulletAttack;
        if ((enemyHealth - (playerDamage - enemyDefense)) > 0) enemyHealth -= (playerDamage - enemyDefense);
        else enemyHealth -= (enemyHealth);
        healthBar.SetHealth(enemyHealth);
        Destroy(collision.gameObject);
    }

    public void EarnCoin()
    {
        if (enemyHealth == 0)
        {
            gamemanager.UserEarnedCoin += enemyCoinAmount;
            gamemanager.SetUserEarnedCoin(gamemanager.UserEarnedCoin);
            gamemanager.SavedEarnedCoinAmount(gamemanager.UserEarnedCoin);
        }
    }

    public int GetEnemyHealth() => enemyHealth;
}
