using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject wallPos;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private List<GameObject> playerCharacters;
    [SerializeField] private GameObject playerSpawn;
    [SerializeField] private GameObject chooseCharObject;

    private Player player;
    private CharacterSelect chooseChar;

    public GameObject playerObj;
    public GameObject wallObj;
    public bool chosen;

    private void Start()
    {
        chooseChar = chooseCharObject.GetComponent<CharacterSelect>();
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (!chooseChar.isChoosing)
            Time.timeScale = 1;

        if (chooseChar.chosen)
        {
            chosen = true;
            wallObj = Instantiate(wallPrefab, wallPos.transform.position, Quaternion.identity);

            playerObj = Instantiate(playerCharacters[chooseChar.chosenCharacter], playerSpawn.transform.position, Quaternion.identity);
            player = playerObj.GetComponent<Player>();

            chooseChar.chosen = false;
        }

        if (chooseChar.isChoosing || player == null)
            return;

        if (player.PlayerLives <= 0)
        {
            player.IsDead = true;
            Time.timeScale = 0;
        }

        if (player.DashComponent != null)
        {
            Dash();
        }
    }

    public void Retry()
    {
        UIController.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Dash()
    {
        Dash dash = player.DashComponent;
        if (dash.isDash)
        {
            if (dash.dashTimer > 0)
            {
                dash.dashTimer -= Time.deltaTime;
            }
            else
            {
                dash.dashGauge = 0;
                dash.dashTimer = 3f;
                dash.isDash = false;
            }
        }
    }
}
