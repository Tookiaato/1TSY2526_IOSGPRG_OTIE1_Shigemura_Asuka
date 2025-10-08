using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [HideInInspector] public float timer;
    public GameObject enemyInstance;

    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject spawner;

    private void Start()
    {
        timer = Random.Range(1f, 2f);
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            enemyInstance = Instantiate(enemies[Random.Range(0,3)], spawner.transform.position, spawner.transform.rotation);
            timer = Random.Range(1f, 2f);
        }
    }
}
