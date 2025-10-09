using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PushableObstacle : Obstacle
{
    [SerializeField] private float pushForce = 5f;
    [SerializeField] private Vector2 pushDirection = Vector2.up;

    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (stackHealth > 0)
            rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
    }
}