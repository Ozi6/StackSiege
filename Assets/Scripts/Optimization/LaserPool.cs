using System.Collections.Generic;
using UnityEngine;

public class LaserWallPool : MonoBehaviour
{
    public static LaserWallPool Instance { get; private set; }

    public GameObject LaserWallPrefab;
    public int poolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject LaserWall = Instantiate(LaserWallPrefab);
            LaserWall.SetActive(false);
            pool.Enqueue(LaserWall);
        }
    }

    public GameObject GetLaserWall(Vector2 position, Quaternion rotation)
    {
        GameObject LaserWall;

        if (pool.Count > 0)
        {
            LaserWall = pool.Dequeue();
        }
        else
        {
            LaserWall = Instantiate(LaserWallPrefab);
        }

        LaserWall.transform.position = position;
        LaserWall.transform.rotation = rotation;
        LaserWall.SetActive(true);

        return LaserWall;
    }

    public void ReturnToPool(GameObject LaserWall)
    {
        LaserWall.SetActive(false);
        pool.Enqueue(LaserWall);
    }
}