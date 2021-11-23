using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _immortalTime = 3f;
    [SerializeField] private float _maxSpeed = 8;
    [SerializeField] private MeshRenderer _meshR;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private GameObject _missile;
    [SerializeField] private GameObject _explosion;

    [HideInInspector] public bool canShoot;
    [HideInInspector] public Vector3 movement;

    private UserInterface _interface;
    private MissilesPool _missilesPool;
    private SoundManager _sound;
    private bool _immortality;
    private float _acceleration = 2;
    
    public Rigidbody rb;
    public ParticleSystem[] engineParticles;

    private void Start()
    {
        _interface = UserInterface.instance;
        _missilesPool = MissilesPool.instance;
        _sound = SoundManager.instance;

        _immortality = false;
        canShoot = true;
        _explosion.SetActive(false);
    }

    private void Update()
    {
        movement.z = Input.GetAxis("Vertical");

        if (Mathf.Abs(movement.z) > 0)
            _acceleration += Time.deltaTime * 5;
        else
            _acceleration -= Time.deltaTime;

        _acceleration = Mathf.Clamp(_acceleration, 2f, _maxSpeed);

        if (movement.z > 0)
        {
            engineParticles[0].Play();
            engineParticles[1].Play();
        }

        if (movement.z < 0)
        {
            engineParticles[2].Play();
            engineParticles[3].Play();
        }

        if (movement.z == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                engineParticles[i].Stop();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(movement.z * transform.forward * _acceleration * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    public IEnumerator Shoot()
    {
        canShoot = false;
        _sound.Shoot();
        
        Missile missile = _missilesPool.SpawnFromPool("Missile", _muzzle.transform.position, _muzzle.transform.rotation);
        
        missile.Setup(_muzzle.forward);
        yield return new WaitForSeconds(1 / 3f);
        canShoot = true;
    }

    private IEnumerator Death()
    {
        _sound.DestroyPlayer();
        _interface.Death();
        _immortality = true;
        rb.isKinematic = true;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        rb.isKinematic = false;

        for (int i = 0; i < _immortalTime; i++)
        {
            _meshR.enabled = false;
            yield return new WaitForSeconds(0.5f);
            _meshR.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        _explosion.transform.parent = transform;
        _explosion.transform.position = transform.position;
        _explosion.SetActive(false);
        _immortality = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid") && !_immortality)
        {
            _explosion.transform.parent = null;
            _explosion.SetActive(true);
            StartCoroutine(Death());
        }
    }
}
