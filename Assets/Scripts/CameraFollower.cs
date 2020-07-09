using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Vector3 _offsetPosition;
    [SerializeField] private Transform _player;

    private void Update()
    {
        var newPosition = _player.position;
        newPosition.y = 0;
        transform.position = newPosition + _offsetPosition;
    }
}
