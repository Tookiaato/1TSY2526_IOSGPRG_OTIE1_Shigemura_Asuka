using UnityEngine;

public class DefaultPlayer : Player
{
    protected override void Start()
    {
        base.Start(); 

        playerLives = 3;
        dashPointsIncrement = 0.05f;
        isDead = false;
        wrongDirection = false;

        rb = GetComponent<Rigidbody2D>();
        powerUp = GetComponent<Powerup>();
        dash = GetComponent<Dash>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
}
