using UnityEngine;

public class AfterImageSpawner : MonoBehaviour
{
    public float spawnInterval = 0.05f;
    public Color afterImageColor = new Color(1f, 1f, 1f, 0.6f);
    public float fadeSpeed = 5f;

    private float timer;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnAfterImage();
            timer = 0f;
        }
    }

    void SpawnAfterImage()
    {
        if (sr == null) return;
        AfterImage img = AfterImagePool.Instance.Get();
        if (img == null) return;
        img.Activate(sr.sprite, transform.position, afterImageColor, fadeSpeed);
        StartCoroutine(ReturnAfterDelay(img, 1f / fadeSpeed));
    }

    System.Collections.IEnumerator ReturnAfterDelay(AfterImage img, float delay)
    {
        yield return new WaitForSeconds(delay);
        img.Deactivate();
        AfterImagePool.Instance.Return(img);
    }
}
