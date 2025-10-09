using UnityEngine;

public abstract class Attack
{
    public float fireRate = 5f;
    public int damage = 1;
    public float nextFireTime = 0f;

    public abstract void Fire(Vector2 position, Quaternion rotation);
}