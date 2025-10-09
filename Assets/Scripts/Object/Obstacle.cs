using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider2D))]
public class Obstacle : MonoBehaviour
{
    public TextMeshPro healthText;
    [SerializeField] private ParticleSystem hitParticles;
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeMagnitude = 0.1f;
    [SerializeField] private Sprite stackSprite;
    [SerializeField] private float damageableDistance = 10f;

    protected int stackHealth;
    protected Vector3 originalPosition;
    private Color myColor;
    private List<GameObject> stackParts = new List<GameObject>();

    protected virtual void Start()
    {
        originalPosition = transform.localPosition;
    }

    protected virtual void Update()
    {
        if (transform.position.y < -12f)
            Destroy(gameObject);
    }

    public virtual void SetStats(int health, Color color)
    {
        stackHealth = health;
        myColor = color;
        GetComponent<SpriteRenderer>().color = color;
        UpdateObstacleVisuals();
        UpdateHealthText();
    }

    private void UpdateObstacleVisuals()
    {
        foreach (var part in stackParts)
            Destroy(part);
        stackParts.Clear();

        int numStacks = Mathf.Min(stackHealth, 15);
        float totalHeight = numStacks * 0.1f;

        BoxCollider2D coll = GetComponent<BoxCollider2D>();
        coll.size = new Vector2(coll.size.x, totalHeight);
        coll.offset = new Vector2(0, totalHeight / 2f);
        healthText.transform.localPosition = new Vector3(healthText.transform.localPosition.x, totalHeight / 2f, healthText.transform.localPosition.z);
        Sprite mainSprite = GetComponent<SpriteRenderer>().sprite;
        for (int i = 1; i < numStacks; i++)
        {
            GameObject stackPart = new GameObject("StackPart" + i);
            stackPart.transform.SetParent(transform);
            stackPart.transform.localPosition = new Vector3(0, i * 0.1f, 0);
            SpriteRenderer sr = stackPart.AddComponent<SpriteRenderer>();
            sr.sprite = mainSprite;
            sr.color = myColor;
            sr.sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;
            sr.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
            stackParts.Add(stackPart);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (Vector3.Distance(transform.position, GameManager.Instance.player.position) > damageableDistance)
            return;

        stackHealth -= damage;
        UpdateHealthText();
        hitParticles.Play();
        StartCoroutine(Shake());
        if (stackHealth <= 0)
            HandleDestruction();
        else
            UpdateObstacleVisuals();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.DamagePlayer(1);
            Destroy(gameObject);
        }
    }

    protected virtual void HandleDestruction()
    {
        GameManager.Instance.ReportObstacleDestroyed(this);
        Destroy(gameObject);
    }

    protected void UpdateHealthText()
    {
        healthText.text = stackHealth.ToString();
    }

    protected IEnumerator Shake()
    {
        Vector3 basePosition = transform.localPosition;
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.localPosition = basePosition + new Vector3(x, y, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = basePosition;
    }
}