using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardsController : MonoBehaviour
{
    [SerializeField] private GameObject _board;
    [SerializeField] private bool _vertical;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 newPos = other.transform.position;

            if (_vertical)
            {
                newPos.x = _board.transform.position.x + other.transform.position.x;
                newPos.z = _board.transform.position.z;
            }
            else
            {
                newPos.x = _board.transform.position.x;
                newPos.z = _board.transform.position.z + other.transform.position.z;
            }

            other.transform.position = newPos;
        }
    }
}
