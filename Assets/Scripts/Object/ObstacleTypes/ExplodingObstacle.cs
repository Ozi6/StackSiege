using UnityEngine;

public class ExplodingObstacle : Obstacle
{
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private int explosionDamage = 2;
    [SerializeField] private ParticleSystem explosionParticles;

    private bool hasExploded = false;

    protected override void HandleDestruction()
    {
        if (hasExploded) return;
        hasExploded = true;
        explosionParticles.Play();
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
                GameManager.Instance.DamagePlayer(explosionDamage);
            else if (hit.TryGetComponent<Obstacle>(out var otherObstacle) && otherObstacle != this)
                otherObstacle.TakeDamage(explosionDamage);
        }

        base.HandleDestruction();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.DamagePlayer(2);
            HandleDestruction();
        }
    }
}
