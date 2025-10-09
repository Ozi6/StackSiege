using UnityEngine;

public class RocketAttack : Attack
{
    public RocketAttack()
    {
        fireRate = 0.5f;
        damage = 2;
    }
    public override void Fire(Vector2 position, Quaternion rotation)
    {
        GameObject obj = RocketPool.Instance.GetRocket(position, rotation);
        Rocket rocket = obj.GetComponent<Rocket>();
        if (rocket != null)
            rocket.damage = damage;
    }
}