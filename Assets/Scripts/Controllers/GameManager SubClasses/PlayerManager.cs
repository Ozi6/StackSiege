using UnityEngine;

public class PlayerManager
{
    public int Health { get; private set; }
    public Transform PlayerTransform { get; }
    private readonly int _maxHealth;

    public event System.Action<int> OnHealthChanged;
    public event System.Action OnPlayerDied;

    public PlayerManager(Transform playerTransform, int startingHealth)
    {
        this.PlayerTransform = playerTransform;
        this._maxHealth = startingHealth;
        this.Health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
        OnHealthChanged?.Invoke(Health);
        if (Health <= 0)
            OnPlayerDied?.Invoke();
    }

    public void ResetHealth()
    {
        Health = _maxHealth;
        OnHealthChanged?.Invoke(Health);
    }
}