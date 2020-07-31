using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _player;
    private readonly Vector3 _enabledAxis = new Vector3(1, 0, 1);

    private void Update()
    {
        transform.position = Vector3.Scale(_player.position, _enabledAxis) + _offset;
    }
}
