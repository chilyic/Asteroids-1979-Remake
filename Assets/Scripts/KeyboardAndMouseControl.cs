using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardAndMouseControl : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotSpeed = 2.5f;    

    private Vector3 _mousePos;
    private float _deltaAngle;
    private float _deltaVector;

    private void OnEnable()
    {
        StartCoroutine(Delta());
    }

    private void OnDisable()
    {
        StopCoroutine(Delta());
    }

    private void Update()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (playerPlane.Raycast(ray, out float hitdist) && Time.timeScale == 1)
        {
            _mousePos = ray.GetPoint(hitdist);
        }

        _target.position = _mousePos;

        Vector3 direction = _target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        
        _player.transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0) && _player.canShoot && Time.timeScale == 1)
        {
            StartCoroutine(_player.Shoot());
        }

        if (_deltaAngle > 0)
        {
            _player.engineParticles[4].Play();
            _player.engineParticles[5].Stop();
        }

        if (_deltaAngle < 0)
        {
            _player.engineParticles[5].Play();
            _player.engineParticles[4].Stop();
        }

        if (Mathf.Abs(_deltaVector) < 0.1f && _deltaAngle < Mathf.Abs(2f))
        {
            _player.engineParticles[4].Stop();
            _player.engineParticles[5].Stop();
        }
    }

    private IEnumerator Delta()
    {
        float curAngle = _player.transform.rotation.eulerAngles.y;
        float curVector = _target.position.magnitude;

        yield return new WaitForSeconds(0.1f);

        float newAngle = _player.transform.rotation.eulerAngles.y;
        float newVector = _target.position.magnitude;

        _deltaAngle = newAngle - curAngle;
        _deltaVector = newVector - curVector;

        StartCoroutine(Delta());
    }
}
