using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed = 300f;
    private float _timeToDestroy = 10f;
    public Vector3 _direction;

    void Start()
    {
        Rigidbody2D _rb = gameObject.GetComponent<Rigidbody2D>();
        _rb.AddForce(_direction.normalized * _speed);
    }


    void Update()
    {
        if ((_timeToDestroy -= Time.deltaTime) <= 0f)
            Destroy(gameObject);
    }
}
