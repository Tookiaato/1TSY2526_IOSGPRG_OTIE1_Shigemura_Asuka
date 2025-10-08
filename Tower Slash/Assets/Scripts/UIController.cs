using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [HideInInspector] public static int score;

    [SerializeField] private TextMeshProUGUI playerLivesUI;
    [SerializeField] private GameObject slider;
    [SerializeField] private Slider dashGaugeSlider;
    [SerializeField] private Image dashGaugeFill;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI finalScoreUI;
    [SerializeField] private Button DashGaugeButton;
    [SerializeField] private GameObject chooseCharObject;
    [SerializeField] private GameObject gameObj;

    private CharacterSelect chooseChar;
    private Player player;
    private Dash dash;
    private GameManager game;

    private void Start()
    {
        chooseChar = chooseCharObject.GetComponent<CharacterSelect>();
        game = gameObj.GetComponent<GameManager>();
        deathMenu.SetActive(false);
    }

    private void Update()
    {
        if (!chooseChar.isChoosing)
        {
            slider.SetActive(true);
            dashGaugeSlider.enabled = true;
            dashGaugeFill.enabled = true;
            scoreUI.enabled = true;
            playerLivesUI.enabled = true;

            dash = game.playerObj.GetComponent<Dash>();
            player = game.playerObj.GetComponent<Player>();

            playerLivesUI.text = "Lives: " + player.PlayerLives;
            scoreUI.text = "Score: " + score;
            dashGaugeSlider.value = dash.dashGauge;

            if (player.IsDead)
            {
                scoreUI.enabled = false;
                playerLivesUI.enabled = false;
                slider.SetActive(false);
                deathMenu.SetActive(true);
                finalScoreUI.text = "Score: " + score;
            }
        }
        else
        {
            slider.SetActive(false);
            dashGaugeSlider.enabled = false;
            dashGaugeFill.enabled = false;
            scoreUI.enabled = false;
            playerLivesUI.enabled = false;
        }
    }

    public void DashGaugePressed()
    {
        if (dash.dashGauge == 1)
        {
            dash.isDash = true;
        }
    }
}
