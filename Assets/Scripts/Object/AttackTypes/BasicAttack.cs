using UnityEngine;

public class BasicAttack : Attack
{
    public override void Fire(Vector2 position, Quaternion rotation)
    {
        GameObject obj = ProjectilePool.Instance.GetProjectile(position, rotation);
        Projectile projectile = obj.GetComponent<Projectile>();
        if (projectile != null)
            projectile.damage = damage;
    }
}