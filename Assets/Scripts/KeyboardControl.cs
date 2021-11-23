using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControl : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _rotSpeed = 1000;

    private void Update()
    {
        _player.movement.y = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && _player.canShoot && Time.timeScale == 1)
        {
            StartCoroutine(_player.Shoot());
        }
        
        if (_player.movement.y > 0)
        {
            _player.engineParticles[4].Play();
            _player.engineParticles[5].Stop();
        }

        if (_player.movement.y < 0)
        {
            _player.engineParticles[5].Play();
            _player.engineParticles[4].Stop();
        }

        if (_player.movement.y == 0)
        {
            _player.engineParticles[4].Stop();
            _player.engineParticles[5].Stop();
        }
    }

    private void FixedUpdate()
    {
        _player.rb.AddTorque(_player.movement * _rotSpeed * Time.fixedDeltaTime);
    }
}
