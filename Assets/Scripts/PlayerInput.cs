using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private GameObject _playerObj;
    private Vector3 _rotationInput;
    private Vector2 _movementInput;
    private Transform _transform;
    private Rigidbody2D _rb;
    public float _speed = 10f;


    void Start()
    {
        _playerObj = (GameObject)GameObject.FindGameObjectWithTag("PlayerSelf");
        _transform = _playerObj.GetComponent<Transform>();
        _rb = _playerObj.GetComponent<Rigidbody2D>();
    }

    // aplicar Behaviors
    void Update()
    {
        GameBehaviors.RotateToMousePos(_transform);
    }

    void FixedUpdate()
    {
        GameBehaviors.MovePlayer(_rb, GetMovement(), _speed);
    }

    // retornar input de movimento
    public Vector2 GetMovement()
    {
        _movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        return _movementInput.normalized;
    }


}
