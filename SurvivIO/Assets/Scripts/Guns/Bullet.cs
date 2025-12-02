using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 5f;
    private Rigidbody2D bulletRigidbody;
    public int bulletDamage;

    private void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bulletRigidbody.linearVelocity = transform.up * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        Unit enemy = collision.gameObject.GetComponent<Unit>();
        Player player = collision.gameObject.GetComponent<Player>();

        if (health != null)
        {
            health.TakeDamage(bulletDamage);

            if (player == null)
            {
                enemy.ManageEnemyHealth();
            }
            else
            {
                GameUI.Instance.UpdatePlayerHealth();
            }
        }
        Destroy(gameObject);
    }
}
