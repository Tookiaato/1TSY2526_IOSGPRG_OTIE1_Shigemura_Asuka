using System.Collections;
using UnityEngine;

public class DashTap : MonoBehaviour
{
    private Vector3 firstPos;
    private Vector3 lastPos;
    private Touch touch;
    private Vector3 touchPosition;
    private float swipeRange = 2f;

    private GameManager game;
    private Player player;
    private WallMovement wall;

    private void Start()
    {
        game = GetComponent<GameManager>();
    }

    private void Update()
    {
        if (game.chosen)
        {
            player = game.playerObj.GetComponent<Player>();
            wall = game.wallObj.GetComponent<WallMovement>();
        }

        if (player == null)
            return;

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
        }

        if (Input.touchCount > 0 && touch.phase == TouchPhase.Began)
        {
            firstPos = touchPosition;
        }
        else if (Input.touchCount > 0 && touch.phase == TouchPhase.Ended)
        {
            lastPos = touchPosition;
            float swipeDistance = Vector3.Distance(firstPos, lastPos);

            if (swipeDistance <= swipeRange && !player.EnemyInRange && !player.IsDead)
            {
                StartCoroutine(TapTime(0.05f));
            }
        }
    }

    private IEnumerator TapTime(float time)
    {
        if (player.DashComponent != null)
        {
            player.DashComponent.isDash = true;
        }

        UIController.score += Random.Range(5, 10);
        yield return new WaitForSeconds(time);

        if (player.CurrentEnemy != null)
        {
            player.CurrentEnemy.speed = 5;
        }

        wall.scrollSpeed = 2.25f;

        if (player.DashComponent != null)
        {
            player.DashComponent.dashTimer = 3f;
            player.DashComponent.isDash = false;
        }
    }
}
