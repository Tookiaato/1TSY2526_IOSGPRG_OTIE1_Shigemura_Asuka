using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    [SerializeField] private bool lootOnly;
    [SerializeField] private int numberOfLoot;
    [SerializeField] private int numberOfEnemies;

    [SerializeField] private Vector3 size;

    [SerializeField] private List<GameObject> _enemyPrefab;
    public List<GameObject> _ammoPrefab;
    public List<GameObject> _gunPrefab;
    public GameObject _healthKitPrefab;

    [SerializeField] private GameObject _lootParent;

    public List<Unit> _enemies;
    public int enemyCount;
    
    public void Start()
    {
        SpawnLootable(numberOfLoot, _gunPrefab, _ammoPrefab, _healthKitPrefab);
        if (!lootOnly)
        {
            SpawnEnemies(numberOfEnemies, _enemyPrefab[0], "Enemy", 100, 2.5f);
        }
    }

    private void SpawnEnemies(int count, GameObject prefab, string name, int maxHealth, float speed)
    {
        if (lootOnly)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            Vector3 randPos = RandomSpawn();

            GameObject enemyGO = Instantiate(prefab, randPos, Quaternion.identity);
            enemyGO.transform.parent = transform;

            Unit unit = enemyGO.GetComponent<Unit>();
            _enemies.Add(unit);

            unit.Initialize(name, maxHealth, speed);
            GameUI.Instance.UpdateEnemyCount();
        }
    }

    private void SpawnLootable(int count, List<GameObject> gunLootPrefab, List<GameObject> ammoLootPrefab, GameObject healthKit)
    {
        for (int i = 0; i < count; i++)
        {
            float randomValue = Random.Range(1f, 100f);
            Vector3 randPos = RandomSpawn();

            switch (randomValue)
            {
                case <= 25f:
                    GameObject gunlootPref = Instantiate(
                        gunLootPrefab[Random.Range(0, gunLootPrefab.Count)], 
                        randPos, 
                        Quaternion.identity
                    );
                    gunlootPref.transform.parent = _lootParent.transform;
                    break;

                case > 90f:
                    GameObject healthKitPref = Instantiate(
                        healthKit,
                        randPos,
                        Quaternion.identity
                    );
                    healthKitPref.transform.parent = _lootParent.transform;
                    break;

                default:
                    GameObject ammolootPref = Instantiate(
                        ammoLootPrefab[Random.Range(0, ammoLootPrefab.Count)], 
                        randPos, 
                        Quaternion.identity
                    );
                    ammolootPref.transform.parent = _lootParent.transform;
                    break;
            }
        }
    }

    private Vector3 RandomSpawn()
    {
        float randomX = Random.Range(-size.x / 2, size.x / 2);
        float randomY = Random.Range(-size.y / 2, size.y / 2);

        Vector3 randomPosition = this.gameObject.transform.position + new Vector3(randomX, randomY, 0);

        return randomPosition;
    }
}
