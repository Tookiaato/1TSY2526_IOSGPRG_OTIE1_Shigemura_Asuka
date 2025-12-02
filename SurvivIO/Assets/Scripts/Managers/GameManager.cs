using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Player _player;
    public Inventory _inventory;

    private void Update()
    {
        if (GameUI.Instance.spawner._enemies.Count <= 0)
        {
            Win();
        }
    }

    public void InitializePlayer(Player player)
    {
        if (_player == null)
        {
            _player = player;
            _inventory = _player.gameObject.GetComponent<Inventory>();

            Health playerHealthScript = _player.gameObject.GetComponent<Health>();
            playerHealthScript._onDeath += GameOver;
        }
        else
        {
            Debug.LogError("Player already set");
        }
    }

    public void GameOver()
    {
        _player = null;
        Debug.Log("Game is Over");
        SceneManager.LoadScene("GameOver");
    }

    public void Win()
    {
        _player = null;
        SceneManager.LoadScene("Win");
    }
}
