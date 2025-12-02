using UnityEngine;

public class Shotgun : Gun
{
    public override void Shoot()
    {
        base.Shoot();

        Debug.Log("Spread Shot");
    }
}
