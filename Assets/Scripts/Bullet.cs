using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    readonly float speed = 5.0f;
    Vector3 _velocity = Vector3.left;
    float time = 0f;

    void Update()
    {
        _velocity.Normalize();

        _velocity.x *= speed * Time.deltaTime;

        transform.Translate(_velocity);

        TimeOut();
    }

    [SerializeField]
    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Boat"))
        {
            Destroy(gameObject);
        }
    }

    void TimeOut() {
        time += Time.deltaTime;
        if (time > 7.5f) 
            Destroy(gameObject);
    }
}
