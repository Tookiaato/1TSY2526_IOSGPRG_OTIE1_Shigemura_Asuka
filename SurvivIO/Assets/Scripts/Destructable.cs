using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private float damageLevel = 0f;  // 0 = no damage, 1 = maximum damage before break

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            damageLevel += 0.1f; // each shot increases redness
    
            damageLevel = Mathf.Clamp01(damageLevel);

            // Increase red tint
            // White (1,1,1) → Red (1,0,0) over time
            _spriteRenderer.color = Color.Lerp(Color.white, Color.red, damageLevel);
        }

        // Once damaged enough, destroy barrel
        if (damageLevel >= 1f)
        {
            Spawner spawner = Spawner.Instance;

            SpawnLoot(Random.Range(1, 4), spawner._gunPrefab, spawner._ammoPrefab, spawner._healthKitPrefab);
            Destroy(gameObject);
        }
    }

    private void SpawnLoot(int count, List<GameObject> gunLootPrefab, List<GameObject> ammoLootPrefab, GameObject healthKit)
    {
        for (int i = 0; i < count; i++)
        {
            float randomValue = Random.Range(1f, 100f);

            if (randomValue <= 10f)
            {
                Instantiate(gunLootPrefab[Random.Range(0, gunLootPrefab.Count)], transform.position, Quaternion.identity);
            }
            else if (randomValue > 40f)
            {
                Instantiate(ammoLootPrefab[Random.Range(0, ammoLootPrefab.Count)], transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(healthKit, transform.position, Quaternion.identity);
            }
        }
    }
}
