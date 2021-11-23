using System.Collections.Generic;
using UnityEngine;

public class MissilesPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public Missile prefab;
        public int size;
    }

    public static MissilesPool instance;

    private void Awake()
    {
        instance = this;
    }

    public Pool pool;
    public Dictionary<string, Queue<Missile>> poolDictionary;
        
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<Missile>>();

        Queue<Missile> objectPool = new Queue<Missile>();

        for (int i = 0; i < pool.size; i++)
        {
            Missile obj = Instantiate(pool.prefab, this.transform);
            objectPool.Enqueue(obj);
        }

        poolDictionary.Add(pool.tag, objectPool);
    }

    public Missile SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        Missile objectToSpawn = poolDictionary[tag].Dequeue();

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
