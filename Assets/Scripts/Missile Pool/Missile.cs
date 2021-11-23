using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 40;
    [SerializeField] private GameObject _fireComplex;

    private void Start()
    {
        _rb.isKinematic = true;
        this.gameObject.SetActive(false);
        _fireComplex.SetActive(false);
    }

    public void Setup(Vector3 direction)
    {
        _rb.isKinematic = false;
        this.gameObject.SetActive(true);
        _rb.AddForce(direction * _speed, ForceMode.Impulse);
    }

    private void BackToPool()
    {
        _rb.isKinematic = true;
        this.gameObject.SetActive(false);
        _fireComplex.transform.parent = this.transform;
        _fireComplex.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            _fireComplex.transform.parent = null;
            _fireComplex.SetActive(true);
            this.gameObject.SetActive(false);

            Invoke(nameof(BackToPool), 4);            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Board"))
        {
            BackToPool();
        }
    }
}
