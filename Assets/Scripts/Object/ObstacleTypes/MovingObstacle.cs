using UnityEngine;

public class MovingObstacle : Obstacle
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveDistance = 2f;

    private Vector3 startPos;

    protected override void Start()
    {
        base.Start();
        startPos = transform.localPosition;
    }

    protected override void Update()
    {
        base.Update();

        float xOffset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.localPosition = startPos + new Vector3(xOffset, 0f, 0f);
    }
}