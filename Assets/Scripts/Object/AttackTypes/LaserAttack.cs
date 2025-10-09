using UnityEngine;

public class LaserWallAttack : Attack
{
    public LaserWallAttack()
    {
        fireRate = 0.5f;
        damage = 3;
    }

    public override void Fire(Vector2 position, Quaternion rotation)
    {
        GameObject LaserWallObj = LaserWallPool.Instance.GetLaserWall(position, rotation);
        LaserWallProjectile LaserWall = LaserWallObj.GetComponent<LaserWallProjectile>();
        if (LaserWall != null)
            LaserWall.damage = damage;
    }
}