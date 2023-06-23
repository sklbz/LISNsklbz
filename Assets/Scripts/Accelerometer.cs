using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    [SerializeField]
    float speed = 10.0f;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update() {
        Vector3 _velocity = Vector3.zero;

        _velocity.x = -Input.acceleration.y;
        _velocity.z = Input.acceleration.x;

        if (_velocity.sqrMagnitude > 1)
        {
            _velocity.Normalize();
        }

        _rb.AddForce(speed * Time.deltaTime * _velocity);
    }
}