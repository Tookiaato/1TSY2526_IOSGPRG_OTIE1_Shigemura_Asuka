using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int ammoMax;
    [SerializeField] private int ammoMin;
    [SerializeField] private Weapon ammoType;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Inventory inventory = collision.gameObject.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.AddAmmo(ammoType, Random.Range(ammoMin, ammoMax));
            Destroy(this.gameObject);
        }
    }
}
