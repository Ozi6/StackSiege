using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : Singleton<ProjectilePool>
{
    public GameObject projectilePrefab;
    public int poolSize = 20;

    private readonly Queue<GameObject> pool = new Queue<GameObject>();

    protected override bool Persistent => false;

    protected override void OnAwake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(projectilePrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetProjectile(Vector3 position, Quaternion rotation)
    {
        GameObject obj = pool.Count > 0 ? pool.Dequeue() : Instantiate(projectilePrefab);
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}