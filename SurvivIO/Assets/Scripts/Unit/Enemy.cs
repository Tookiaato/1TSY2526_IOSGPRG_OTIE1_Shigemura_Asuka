using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private void Start()
    {
        targetDestination = transform.position;
        InsantiateEnemyHealthBar();

        _currentGun = _guns[Random.Range(0, _guns.Count)].GetComponent<Gun>(); // Select a random gun

        Vector3 gunOffset = new Vector3(0.5f, 0f, 0f);
        Quaternion gunRotation = Quaternion.Euler(0, 0, -90);

        GameObject equippedGunObj = Instantiate(_currentGun.gameObject, transform.position + gunOffset, gunRotation, transform);

        _currentGun = equippedGunObj.GetComponent<Gun>();
        _currentGun._isInfiniteAmmo = true;
        _currentGun.gunObj = transform.gameObject; // Bullet spawn position
    }

    private void FixedUpdate()
    {
        MoveTowardsTarget(5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        unit = collision.GetComponent<Unit>();

        if (unit != null)
        {
            isWithinRange = true;
            targetList.Add(unit);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        unit = other.GetComponent<Unit>();
        targetList.Remove(unit);
    }

    private void OnDestroy()
    {
        targetList.Remove(unit);
    }

    public override void Shoot()
    {
        base.Shoot();
    }
}