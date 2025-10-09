using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Projectile : MonoBehaviour
{
    public float speed = 3f;
    public int damage = 1;
    public float lifetime = 3f;
    public LayerMask obstacleLayer;

    protected Rigidbody2D rb;
    protected float lifeTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        rb.linearVelocity = Vector2.up * speed;
        lifeTimer = lifetime;
    }

    protected virtual void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
            DestroyProjectile();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & obstacleLayer) != 0)
        {
            Obstacle obstacle = other.GetComponent<Obstacle>();
            if (obstacle != null)
                obstacle.TakeDamage(damage);
            DestroyProjectile();
        }
    }

    public virtual void DestroyProjectile()
    {
        ProjectilePool.Instance.ReturnToPool(gameObject);
    }
}