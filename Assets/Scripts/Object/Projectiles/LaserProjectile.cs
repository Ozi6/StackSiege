using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class LaserWallProjectile : MonoBehaviour
{
    public float speed = 12f;
    public int damage = 3;
    public float lifetime = 2f;
    public float pierceCooldown = 0.5f;
    public LayerMask obstacleLayer;

    private Rigidbody2D rb;
    private float lifeTimer;
    private Dictionary<Obstacle, float> hitObstacles = new Dictionary<Obstacle, float>();

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
    }

    void OnEnable()
    {
        rb.linearVelocity = transform.up * speed;
        lifeTimer = lifetime;
        hitObstacles.Clear();
    }

    void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            DestroyProjectile();
            return;
        }
        List<Obstacle> toRemove = new List<Obstacle>();
        foreach (var kvp in hitObstacles)
            if (Time.time - kvp.Value > pierceCooldown)
                toRemove.Add(kvp.Key);
        foreach (var obstacle in toRemove)
            hitObstacles.Remove(obstacle);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & obstacleLayer) != 0)
        {
            Obstacle obstacle = other.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                if (!hitObstacles.ContainsKey(obstacle))
                {
                    obstacle.TakeDamage(damage);
                    hitObstacles[obstacle] = Time.time;
                }
            }
        }
    }

    public void DestroyProjectile()
    {
        LaserWallPool.Instance.ReturnToPool(gameObject);
    }
}