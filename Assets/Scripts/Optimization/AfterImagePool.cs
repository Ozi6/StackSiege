using UnityEngine;
using System.Collections.Generic;

public class AfterImagePool : MonoBehaviour
{
    public static AfterImagePool Instance;
    public GameObject afterImagePrefab;
    public int poolSize = 30;

    private Queue<AfterImage> pool = new Queue<AfterImage>();

    void Awake()
    {
        Instance = this;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(afterImagePrefab, transform);
            var ai = obj.GetComponent<AfterImage>();
            ai.Deactivate();
            pool.Enqueue(ai);
        }
    }

    public AfterImage Get()
    {
        if (pool.Count == 0)
            return null;

        AfterImage img = pool.Dequeue();
        return img;
    }

    public void Return(AfterImage img)
    {
        pool.Enqueue(img);
    }
}
