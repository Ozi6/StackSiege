using UnityEngine;

public class SpreadAttack : Attack
{
    public float spreadAngle = 20f;

    public SpreadAttack()
    {
        fireRate = 1f;
        damage = 1;
    }

    public override void Fire(Vector2 position, Quaternion rotation)
    {
        for (int i = -1; i <= 1; i++)
        {
            float angleOffset = i * (spreadAngle / 2f);
            Quaternion adjustedRotation = rotation * Quaternion.Euler(0, 0, angleOffset);
            GameObject projectileObj = ProjectilePool.Instance.GetProjectile(position, adjustedRotation);
            Projectile projectile = projectileObj.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.damage = damage;
                Vector2 direction = adjustedRotation * Vector2.up;
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                    rb.linearVelocity = direction * projectile.speed;
            }
        }
    }
}