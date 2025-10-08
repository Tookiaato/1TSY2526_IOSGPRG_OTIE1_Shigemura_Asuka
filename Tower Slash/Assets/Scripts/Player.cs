using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected int playerLives = 3;
    [SerializeField] protected int maxPlayerLives = 3;
    [SerializeField] protected float gravityToWall;

    protected bool isDead;
    protected bool enemyInRange;
    protected bool wrongDirection;

    protected Rigidbody2D rb;
    protected Slash slashDir;
    protected Enemy enemy;
    protected Powerup powerUp;
    protected Dash dash;
    protected CircleCollider2D circleCollider;

    protected float dashPointsIncrement = 0.05f;

    public int PlayerLives
    {
        get => playerLives;
        set => playerLives = Mathf.Clamp(value, 0, maxPlayerLives);
    }

    public bool IsDead
    {
        get => isDead;
        set => isDead = value;
    }

    public bool EnemyInRange => enemyInRange;
    public bool WrongDirection => wrongDirection;

    public Dash DashComponent => dash; 
    public Enemy CurrentEnemy => enemy;

    protected virtual void Start()
    {
        isDead = false;
        wrongDirection = false;

        rb = GetComponent<Rigidbody2D>();
        powerUp = GetComponent<Powerup>();
        dash = GetComponent<Dash>();
        slashDir = GetComponent<Slash>();
        circleCollider = GetComponent<CircleCollider2D>();

        if (slashDir != null)
            slashDir.slashDirection = 0;
    }

    protected virtual void Update()
    {
        if (isDead)
            return;

        if (playerLives <= 0)
            playerLives = 0;

        circleCollider.radius = dash != null && dash.isDash ? 15f : 6f;

        if (enemy != null)
        {
            if (wrongDirection)
            {
                playerLives--;
                wrongDirection = false;
            }

            if (slashDir.slashDirection != 0 && enemy.deathDirection != slashDir.slashDirection && slashDir.slashDirection != 0.05f)
            {
                wrongDirection = true;
                slashDir.slashDirection = 0;
            }

            if (enemy.deathDirection == slashDir.slashDirection)
                enemy.enemyHp = 0;

            if (dash.isDash)
                enemy.speed = Time.unscaledTime;

            if (enemy.enemyHp == 0)
            {
                enemyInRange = false;
                powerUp.PowerupChance();
                IncreaseDashGauge(dashPointsIncrement);
                UIController.score += Random.Range(15, 30);
            }
        }
        else
        {
            wrongDirection = false;
        }
    }

    protected virtual void FixedUpdate()
    {
        rb.linearVelocity = Vector2.right * gravityToWall * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log("enemy in range");
            enemyInRange = true;
            slashDir.slashDirection = 0.05f;

            enemy.secondArrow.SetActive(true);
            enemy.arrow.SetActive(false);
        }
    }

    protected void IncreaseDashGauge(float value)
    {
        dash.dashGauge += value;
    }
}
