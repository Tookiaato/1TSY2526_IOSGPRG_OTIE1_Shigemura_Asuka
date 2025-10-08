using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<GameObject> arrows;

    private SpriteRenderer render;
    private Dash dash;

    public GameObject arrow;
    public GameObject secondArrow;

    public bool isDead;
    public float deathDirection;
    public float enemyHp;
    public float speed;

    private void Start()
    {
        deathDirection = Random.Range(1, 5);
        int randomIdx = Random.Range(0, arrows.Count);

        Quaternion arrowRotation = transform.rotation;

        switch (deathDirection)
        {
            case 1:
                arrowRotation = Quaternion.Euler(0, 0, -90); // down
                break;
            case 2:
                arrowRotation = Quaternion.Euler(0, 0, 90); // up
                break;
            case 3:
                arrowRotation = Quaternion.Euler(0, 0, 0); // right
                break;
            case 4:
                arrowRotation = Quaternion.Euler(0, 0, 180); // left
                break;
        }

        if (arrows[randomIdx] == arrows[2])
        {
            secondArrow = Instantiate(arrows[0], transform.position + new Vector3(-2, 0, 0), arrowRotation);
            secondArrow.transform.SetParent(transform, true);
            secondArrow.SetActive(false);
        }
        else if (arrows[randomIdx] == arrows[1])
        {
            render = arrows[1].GetComponent<SpriteRenderer>();
            render.flipX = true;
            arrow = Instantiate(arrows[randomIdx], transform.position + new Vector3(-2, 0, 0), arrowRotation);
            arrow.transform.SetParent(transform, true);
        }

        arrow = Instantiate(arrows[randomIdx], transform.position + new Vector3(-2, 0, 0), arrowRotation);
        arrow.transform.SetParent(transform, true);
    }

    private void Update()
    {
        if (dash != null)
        {
            speed = dash.isDash ? 20f : 5f;
        }

        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (enemyHp <= 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        dash = other.collider.GetComponent<Dash>();

        if (dash != null && dash.isDash)
        {
            UIController.score += Random.Range(15, 30);
            Destroy(gameObject);
            return;
        }

        Player player = other.collider.GetComponent<Player>();
        if (player != null && (dash == null || !dash.isDash))
        {
            player.PlayerLives--;
        }
    }
}
