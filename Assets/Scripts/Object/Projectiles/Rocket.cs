using UnityEngine;

public class Rocket : Projectile
{
    public float wiggleFrequency = 1.5f;
    public float wiggleAmplitude = 2f;
    public float explosionRadius = 2f;
    public float rotationSpeed = 8f;

    private float timeOffset;
    private float startX;

    protected override void OnEnable()
    {
        speed = 4f;
        base.OnEnable();
        timeOffset = Random.Range(0f, 2f * Mathf.PI);
        startX = transform.position.x;
    }

    protected override void Update()
    {
        base.Update();
        float targetX = startX + Mathf.Sin((Time.time + timeOffset) * wiggleFrequency) * wiggleAmplitude;
        float newY = transform.position.y + speed * Time.deltaTime;
        float newX = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * 10f);
        Vector2 velocity = new Vector2((newX - transform.position.x) / Time.deltaTime, speed);
        transform.position = new Vector2(newX, newY);
        if (velocity.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(velocity.x, velocity.y) * Mathf.Rad2Deg;
            float currentAngle = transform.eulerAngles.z;
            float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotationSpeed);
            transform.rotation = Quaternion.Euler(0, 0, -newAngle);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & obstacleLayer) != 0)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, obstacleLayer);
            foreach (var hit in hits)
            {
                Obstacle obstacle = hit.GetComponent<Obstacle>();
                if (obstacle != null)
                    obstacle.TakeDamage(damage);
            }
            DestroyProjectile();
        }
    }

    public override void DestroyProjectile()
    {
        RocketPool.Instance.ReturnToPool(gameObject);
    }
}