using UnityEngine;

public class AfterImage : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    private float alpha;
    private float fadeSpeed;
    private bool active;

    void Update()
    {
        if (!active) return;

        alpha -= fadeSpeed * Time.deltaTime;
        Color c = sr.color;
        c.a = alpha;
        sr.color = c;

        if (alpha <= 0f)
            Deactivate();
    }

    public void Activate(Sprite sprite, Vector3 position, Color color, float fadeSpeed)
    {
        active = true;
        alpha = color.a;
        sr.sprite = sprite;
        sr.color = color;
        transform.position = position;
        this.fadeSpeed = fadeSpeed;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        active = false;
        gameObject.SetActive(false);
    }
}
