using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public BaseAsteroid prefab;
        public int size;
    }

    public static AsteroidsPool instance;

    private void Awake()
    {
        instance = this;
    }

    public Pool[] pool;
    public Dictionary<string, Queue<BaseAsteroid>> poolDictionary;
        
    private void Start()
    {        
        poolDictionary = new Dictionary<string, Queue<BaseAsteroid>>(pool.Length);

        for (int j = 0; j < pool.Length; j++)
        {
            Queue<BaseAsteroid> objectPool = new Queue<BaseAsteroid>();

            for (int i = 0; i < pool[j].size; i++)
            {
                BaseAsteroid obj = Instantiate(pool[j].prefab, this.transform);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool[j].tag, objectPool);            
        }
    }

    public BaseAsteroid SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }
        
        BaseAsteroid objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
        }
        
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
