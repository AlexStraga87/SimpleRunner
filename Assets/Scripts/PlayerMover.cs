using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float speed = 7;
    private Rigidbody2D _rigidBody;
    private const float _groundYPosition = 0.65f;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && transform.position.y < _groundYPosition)
        {
            _rigidBody.AddForce(new Vector2(0, 500));
        }
    }
}
