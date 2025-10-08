using UnityEngine;

public class SpeedPlayer : Player
{
    protected override void Start()
    {
        base.Start(); // Run base initialization first

        playerLives = 5;
        dashPointsIncrement = 0.05f;
        isDead = false;
        wrongDirection = false;

        // Ensure components are fetched (in case base.Start() is modified later)
        rb = GetComponent<Rigidbody2D>();
        powerUp = GetComponent<Powerup>();
        dash = GetComponent<Dash>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
}
