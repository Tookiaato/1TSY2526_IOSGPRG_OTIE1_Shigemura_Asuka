using UnityEngine;

public class AutomaticRifle: Gun
{
    public override void Shoot()
    {
        base.Shoot();

        Debug.Log("Multishot");
    }
}
