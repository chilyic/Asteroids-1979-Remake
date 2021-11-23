using UnityEngine;

public class BaseAsteroid : MonoBehaviour
{
    public static BaseAsteroid instance { get; private set; }

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform[] _crashPoint;
    [SerializeField] private string _tagForBrokenAsteroid;
    [SerializeField] private float _speed = 8;
    [HideInInspector] public SoundManager sound;

    private UserInterface userInterface;    
    private Vector3 _direction;
    private AsteroidsPool _asteroidsPool;

    public AudioSource knockSound;
    public int score = 20;

    private void Awake()
    {
        instance = this;
        knockSound.enabled = false;
    }

    private void Start()
    {
        userInterface = UserInterface.instance;
        sound = SoundManager.instance;
        _asteroidsPool = AsteroidsPool.instance;

        _speed = Random.Range(_speed - 2, _speed + 2);
        _rb.isKinematic = true;
        this.gameObject.SetActive(false);
        knockSound.enabled = true;

        InvokeRepeating(nameof(Distance), 5, 3);
    }

    public void Setup(Vector3 direction)
    {
        _rb.isKinematic = false;
        this.gameObject.SetActive(true);

        _direction = direction;

        transform.eulerAngles = new Vector3(0, Random.value * 360, 0);
        _rb.AddForce(_direction * _speed);
    }

    public void ScoreAdded(int score) => userInterface.ScoreAdded(score);

    public void Broke()
    {
        if (_crashPoint.Length != 0)
        {
            BaseAsteroid asteroid_1 = _asteroidsPool.SpawnFromPool(_tagForBrokenAsteroid, _crashPoint[0].position, Quaternion.identity);
            BaseAsteroid asteroid_2 = _asteroidsPool.SpawnFromPool(_tagForBrokenAsteroid, _crashPoint[1].position, Quaternion.identity);

            asteroid_1.Setup(_direction);
            asteroid_2.Setup(_direction);
        }
    }

    private void Distance()
    {
        if (Vector3.Distance(Vector3.zero, transform.position) > Screen.width / 4 ||
            Vector3.Distance(Vector3.zero, transform.position) > Screen.height / 4)
        {
            BackToPool();
        }
    }

    public void BackToPool()
    {
        _rb.isKinematic = true;
        this.gameObject.SetActive(false);
    }
}
    
