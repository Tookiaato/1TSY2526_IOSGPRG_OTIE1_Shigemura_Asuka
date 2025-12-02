using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKitPickup : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Inventory inventory = collision.gameObject.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.PickupHealthKit();
            Destroy(gameObject);
        }
    }
}
