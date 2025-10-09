using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public float speed = 5f;
    public float upwardSpeed = 1.2f;
    public Transform firePoint;
    public float movementBounds = 8f;

    public List<Attack> attacks = new List<Attack>();

    private Camera mainCamera;

    protected override bool Persistent => false;

    void Start()
    {
        mainCamera = Camera.main;
        attacks.Add(new BasicAttack { fireRate = 2.5f, damage = 1 });
    }

    void Update()
    {
        transform.position += Vector3.up * upwardSpeed * Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            HandleControlledMovement();
            HandleShooting();
        }
    }

    void HandleControlledMovement()
    {
        Vector3 touchPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float direction = 0f;

        if (touchPos.x < transform.position.x)
            direction = -1f;
        else if (touchPos.x > transform.position.x)
            direction = 1f;

        Vector3 newPos = transform.position + Vector3.right * direction * speed * Time.deltaTime;
        newPos.x = Mathf.Clamp(newPos.x, -movementBounds, movementBounds);
        transform.position = newPos;
    }

    void HandleShooting()
    {
        foreach (var attack in attacks)
        {
            if (Time.time >= attack.nextFireTime)
            {
                attack.Fire(firePoint.position, firePoint.rotation);
                attack.nextFireTime = Time.time + 1f / attack.fireRate;
            }
        }
    }
}