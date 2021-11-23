using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnDistance = 70;
    [SerializeField] private float _trajectory = 15;
    [SerializeField] private string[] _tag;
    [SerializeField] private float _delay = 2;
    [SerializeField] private float _repeatRate = 3;

    private AsteroidsPool _asteroidsPool;
    
    private void Start()
    {
        _asteroidsPool = AsteroidsPool.instance;

        InvokeRepeating(nameof(Spawn), _delay, _repeatRate);
    }

    public void Spawn()
    {
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * _spawnDistance;
        
        spawnDirection.z = spawnDirection.y;
        spawnDirection.y = 0;

        Vector3 spawnPoint = transform.position + spawnDirection;

        float variance = Random.Range(-_trajectory, _trajectory);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.up);

        BaseAsteroid asteroid = _asteroidsPool.SpawnFromPool(_tag[Random.Range(0, _tag.Length)], spawnPoint, rotation);

        asteroid.Setup(rotation * - spawnDirection);
    }
}
