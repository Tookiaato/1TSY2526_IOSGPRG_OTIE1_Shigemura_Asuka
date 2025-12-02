using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [SerializeField] private GameObject gunObj;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Gun gun = gunObj.GetComponent<Gun>();

        Inventory inventory = collision.gameObject.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.PickUpWeapon(gun);

            Destroy(this.gameObject);
        }
    }
}
